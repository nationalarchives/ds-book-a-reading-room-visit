USE [KewBooking]
GO
/****** Object:  StoredProcedure [dbo].[InsertBulkOrdersIntoDorisBulkRequisitions]    Script Date: 12/05/2021 20:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		acurry
-- Create date: 28 March 2021
-- Description:	Get completed bulk Booking Orders from KewBooking and insert into Doris requisitions. Then Update inserted orders as requisitioned.
--
-- acurry 11 May 2021 
-- update to get full reader ticket if temporary ticket has been updated in prologon and therefore temp ticket stored in KBS no longer valid in Doris
-- don't get 'missing' order prior to 13 May as too late the user has already been.
-- =============================================
ALTER PROCEDURE [dbo].[InsertBulkOrdersIntoDorisBulkRequisitions] 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE  @booking_id integer,
	@previous_booking_id integer,
	@due_date datetime,
	@user_id integer,
	@user_ticket integer,
	@fullname nvarchar(255),
	@user_name nvarchar(255),
	@user_group integer,
	@order_date datetime,
	@order_document_id integer,
	@doc_id integer,
	@doc_ref nvarchar(50),
	@letter_code nvarchar(20),
	@class_no integer,
	@subclass_no integer,
	@piece_ref nvarchar(20),
	@item_ref nvarchar(20)

	Set @previous_booking_id = 0

	DECLARE Requisition_Cursor CURSOR FOR 
	select b.Id,
	VisitStartDate,
	isnull(ur.user_id, nur.user_id) as user_id,
	isnull(ur.user_ticket, nur.user_ticket) as user_ticket,
	b.[FirstName] + ' ' + b.LastName,
	isnull(ur.user_fname, nur.user_fname) + ' ' + isnull(ur.user_sname, nur.user_sname),
	isnull(ur.user_type, nur.user_type),
	convert(datetime,CreatedDate,103),
	od.Id,
	od.PieceId,
    CASE   
        WHEN od.PieceId > 0 THEN od.[DocumentReference] + ' (Piece)' 
        WHEN od.PieceId < 0 THEN od.[DocumentReference] + ' (Item)'
        ELSE od.[DocumentReference] + ' (Exception)'
    END  as [DocumentReference], 
	[LetterCode],
	[ClassNumber],
	[SubClassNumber],
	[PieceReference],
	[ItemReference]
	From KewBooking.dbo.Bookings b
	inner join KewBooking.dbo.OrderDocuments od on b.Id = od.BookingId
	left outer join [prologon].[dbo].[user_reader] ur on b.readerticket = ur.user_ticket
	left outer join [prologon].[dbo].[user_reader_old_tickets] our on b.readerticket = our.user_ticket
	left outer join [prologon].[dbo].[user_reader] nur on our.user_id = nur.user_id
	where  b.[BookingTypeId] = 2 -- bulk order
	and b.BookingStatusId = 2 --Completed
	and b.VisitStartDate > '2021-05-12 00:00:00.0000000' -- in order not to pick up old 'missed' temp ticket orders
	and isnull(od.requisitioned,0) = 0
	and od.[IsReserve] = 0
	order by 1,2,4

	OPEN Requisition_Cursor

	FETCH NEXT FROM Requisition_Cursor 
	INTO  @booking_id,
	@due_date,
	@user_id,
	@user_ticket,
	@fullname,
	@user_name,
	@user_group,
	@order_date,
	@order_document_id,
	@doc_id,
	@doc_ref,
	@letter_code,
	@class_no,
	@subclass_no,
	@piece_ref,
	@item_ref


	WHILE @@FETCH_STATUS = 0
	BEGIN
		BEGIN TRANSACTION;   
		if @doc_id > 0
			begin
			insert into doris.dbo.requisitions_bulk
			(sitename,
			floor,
			wing,
			bay,
			specific_location,
			ou_id,
			wing_id,
			avail_cond_bit_code,
			due_date,
			user_id,
			user_ticket,
			fullname,
			user_name,
			order_date,
			doc_ref,
			letter_code,
			class_no,
			subclass_no,
			piece_ref,
			item_ref,
			production_type,
			req_status,
			req_type_code,
			pop,
			pagerpig,
			completion_code,
			req_comment,
			ou_type,
			user_group,
			obo_user_name)
			SELECT l.site,
			l.floor,
			l.wing,
			l.bay,
			o.specific_location,
			o.ou_id,
			b.wing_id,
			t.bit_code,
			@due_date,
			@user_id,
			@user_ticket,
			@fullname,
			@user_name,
			@order_date,
			--@doc_id,
			@doc_ref,
			@letter_code,
			@class_no,
			@subclass_no,
			@piece_ref,
			@item_ref,
			20 as production_type,
			20 as req_status,
			50 as req_type_code,
			'Special Productions' as pop,
			'BULK' as pagerpig,
			0 as completion_code,
			null as req_comment,
			1 as ou_type,
			@user_group,
			'doris sp' as obo_user_name
			from doris.dbo.o_unit o
			inner join doris.dbo.v_location l on o.bay_id = l.bay_id
			inner join doris.dbo.bay b on b.bay_id = l.bay_id
			left outer join doris.dbo.avail_condition a on a.ou_id = o.ou_id
			left outer join doris.dbo.avail_cond_type t on a.avail_cond_id = t.id
			where o.ou_id = @doc_id
			and o.type = 1
			end
		else
		begin
			insert into doris.dbo.requisitions_bulk
			(sitename,
			floor,
			wing,
			bay,
			specific_location,
			ou_id,
			wing_id,
			avail_cond_bit_code,
			due_date,
			user_id,
			user_ticket,
			fullname,
			user_name,
			order_date,
			doc_ref,
			letter_code,
			class_no,
			subclass_no,
			piece_ref,
			item_ref,
			production_type,
			req_status,
			req_type_code,
			pop,
			pagerpig,
			completion_code,
			req_comment,
			ou_type,
			user_group,
			obo_user_name)
			select l.site,
			l.floor,
			l.wing,
			l.bay,
			o.specific_location,
			o.ou_id,
			b.wing_id,
			t.bit_code,			@due_date,
			@user_id,
			@user_ticket,
			@fullname,
			@user_name,
			@order_date,
			--@doc_id,
			@doc_ref,
			@letter_code,
			@class_no,
			@subclass_no,
			@piece_ref,
			@item_ref,
			20 as production_type,
			20 as req_status,
			50 as req_type_code,
			'Special Productions' as pop,
			'BULK' as pagerpig,
			0 as completion_code,
			null as req_comment,
			2 as ou_type,
			@user_group,
			'doris sp' as obo_user_name
			from doris.dbo.o_unit o
			inner join doris.dbo.v_location l on o.bay_id = l.bay_id
			inner join doris.dbo.bay b on b.bay_id = l.bay_id
			left outer join doris.dbo.avail_condition a on a.ou_id = o.ou_id
			left outer join doris.dbo.avail_cond_type t on a.avail_cond_id = t.id
			where o.ou_id = @doc_id * -1
			and o.type = 2
			end

			if @@ROWCOUNT > 0
			Begin
				PRINT 'Requisition inserted for ' + cast(@doc_id	as varchar)

				update KewBooking.dbo.OrderDocuments
				set Requisitioned = 1
				where Id = @order_document_id
				PRINT 'Booking status updated for booking ' + cast(@booking_id	as varchar) + ' order documnent id ' + cast(@order_document_id as varchar) 
			End
			Else
				PRINT 'No Insert for BookingID ' + cast(@booking_id	as varchar) + ' order documnent id ' + cast(@order_document_id as varchar) 

		COMMIT TRANSACTION; 

	   FETCH NEXT FROM Requisition_Cursor 
	   INTO @booking_id,
		@due_date,
		@user_id,
		@user_ticket,
		@fullname,
		@user_name,
		@user_group,
		@order_date,
		@order_document_id,
		@doc_id,
		@doc_ref,
		@letter_code,
		@class_no,
		@subclass_no,
		@piece_ref,
		@item_ref

	END

	CLOSE Requisition_Cursor
	DEALLOCATE Requisition_Cursor
END

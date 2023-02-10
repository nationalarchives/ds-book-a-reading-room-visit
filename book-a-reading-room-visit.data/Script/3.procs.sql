USE [KewBooking]
GO

/****** Object:  StoredProcedure [dbo].[proc_change_booked_seat]    Script Date: 10/02/2023 14:57:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- ==============================================================
-- Author:		Brian O Reilly
-- Create date: 27th April 2021
-- Description:	Updates the booked seat for	a reading room visit. 
--				Ensures	that no other process can update 
--				the seat for a booking on the same
--				date during the	transaction (though new bookings
--				for the date can be added).
-- ==============================================================
CREATE PROCEDURE [dbo].[proc_change_booked_seat] 
	@booking_id INT,
	@new_seat_id INT,
	@comments nvarchar(MAX),
	@updated_by nvarchar(50)
AS

DECLARE @visit_start_date datetime;
DECLARE @visit_start_date_string varchar(50);

DECLARE @bookings_for_date TABLE
(
	booking_id INT NOT NULL PRIMARY KEY,
	seat_id INT NOT NULL,
	booking_status_id INT NOT NULL
)


BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET XACT_ABORT ON;
	
	SELECT @visit_start_date = DATEADD(DAY, 0, DATEDIFF(DAY, 0,  VisitStartDate)) FROM Bookings WHERE Id = @booking_id;
	
	SET TRANSACTION ISOLATION LEVEL REPEATABLE READ;
	BEGIN TRANSACTION;

		-- We need to read all the bookings with seat ids for the date in order to prevent possible inserts or updates during the transaction which would
		-- result in in another booking for the same seat id and date.  If we just use EXISTS to check no rows are returned then we would need a
		-- serialiazable transaction.  Note that this prevents any seat updates for bookings on the same date during the transaction (though not the
		-- addition of new bookings).
		INSERT INTO @bookings_for_date 
			SELECT Id, SeatId, BookingStatusId FROM [dbo].[Bookings] WHERE DATEADD(DAY, 0, DATEDIFF(DAY, 0,  VisitStartDate)) = (@visit_start_date) AND BookingStatusId <> 3;

		IF exists (SELECT seat_id FROM @bookings_for_date WHERE seat_id = @new_seat_id)
			BEGIN
				SELECT @visit_start_date_string = convert(varchar, @visit_start_date, 23);
				RAISERROR('The seat with id %d is already taken on %s.', 16, 1, @new_seat_id, @visit_start_date_string);
			END
		ELSE
			BEGIN
				UPDATE [dbo].[Bookings]
					SET SeatId = @new_seat_id,
						Comments = CASE ISNULL(Comments, '')
									WHEN '' THEN @comments
									ELSE Comments + ' ' + @comments
									END,
						LastModifiedBy = @updated_by
					WHERE Id = @booking_id;
			END;

	COMMIT TRANSACTION;


END TRY
BEGIN CATCH  
    IF (XACT_STATE()) <> 0
      ROLLBACK TRANSACTION;

       DECLARE @Message varchar(MAX) = ERROR_MESSAGE(),
        @Severity int = ERROR_SEVERITY(),
        @State smallint = ERROR_STATE();

		RAISERROR (@Message, @Severity, @State);
END CATCH;
GO


USE [KewBooking]
GO

/****** Object:  StoredProcedure [dbo].[proc_delete_expired_bookings]    Script Date: 10/02/2023 15:00:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[proc_delete_expired_bookings]
(
	@minute_count INT = 20
)
AS
SET NOCOUNT ON;

DELETE FROM [dbo].[Bookings]
WHERE [ReaderTicket] IS NULL AND DATEDIFF(minute, CreatedDate, GETUTCDATE()) > 20; 
GO




USE [KewBooking]
GO

/****** Object:  StoredProcedure [dbo].[proc_reader_tickets_upgrade_temp_to_full]    Script Date: 10/02/2023 14:59:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ==========================================================
-- Author:		BN O Reilly
-- Create date: 23 November 2022
-- Description:	Upgrades temporary reader tickets
--              to full in KewBooking by retrieving
--				the user's full ticket from prologon
--------------------------------------------------------------
-- Changes
--------------------------------------------------------------
-- By: BN O Reilly
-- Date: 13 December 2022
-- Details: 1. Corrected prologon table to user_reader_old_tickets
-- from arch_user_reader_old_tickets.  2. Changed WHERE clause
-- to look for tickets reissued in tha last 3 months instead of
-- filtering on tickets with negative numbers.  This ensures that
-- full ticket renewals are captured in addition to temporary
-- ticket upgrades.
--------------------------------------------------------------
-- Amendements
-- By: BN O Reilly
-- Date: 19 December 2022
-- Details: Added optional parameter to override the 3 month
--          date check
--------------------------------------------------------------
-- Amendements
-- By: A Curry
-- Date: 08 February 2022
-- Details: Changed to only update future bookings with new ticket number and to deal 
-- with changes to full ticket numbers, not just temporary
-- Also changed to use a cursor to update each record individually. This does away
-- with the need for a trigger on the bookings table. The cursor will be slower but
-- after the initial full load there isn't expected to be many ticket changes per day.
-- ============================================================
CREATE   PROCEDURE [dbo].[proc_reader_tickets_upgrade_temp_to_full]
AS
BEGIN

	SET NOCOUNT ON

	DECLARE @booking_id int,
		   @old_reader_ticket int,
		   @new_reader_ticket int

	DECLARE Ticket_Cursor CURSOR FOR 
		SELECT b.Id,
		   b.ReaderTicket,
		   nur.user_ticket as new_user_ticket
		FROM KewBooking.dbo.Bookings b
       INNER join [prologon].[dbo].[user_reader_old_tickets] ot on b.readerticket = ot.user_ticket
       INNER join [prologon].[dbo].[user_reader] nur on ot.user_id = nur.user_id

		WHERE b.VisitStartDate > getdate()
		AND nur.user_ticket <> b.ReaderTicket

	OPEN Ticket_Cursor

	FETCH NEXT FROM Ticket_Cursor 
	INTO @booking_id 
		,@old_reader_ticket
		,@new_reader_ticket


	WHILE @@FETCH_STATUS = 0
	BEGIN

		UPDATE Bookings
		SET ReaderTicket = @new_reader_ticket
		WHERE Bookings.Id = @booking_id

		IF @@ERROR=0
		BEGIN
			INSERT INTO [dbo].[tbl_reader_ticket_upgrades]
			(
				booking_id,
				reader_ticket_old,
				reader_ticket_new
			)
			SELECT @booking_id 	,@old_reader_ticket	,@new_reader_ticket
		END

	   FETCH NEXT FROM Ticket_Cursor 
	   INTO  @booking_id
   		,@old_reader_ticket
		,@new_reader_ticket

	END

	CLOSE Ticket_Cursor
	DEALLOCATE Ticket_Cursor

END
GO



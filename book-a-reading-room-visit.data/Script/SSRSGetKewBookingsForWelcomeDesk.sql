USE [KewBooking]
GO
/****** Object:  StoredProcedure [dbo].[SSRSGetKewBookingsForWelcomeDesk]    Script Date: 12/05/2021 20:52:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		acurry
-- Create date: 30 March 2021
-- Description:	Get Completed Kew Bookings for Welcome Desk report
-- =============================================
ALTER PROCEDURE [dbo].[SSRSGetKewBookingsForWelcomeDesk]
	-- Add the parameters for the stored procedure here
	@dateofvisit datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

  	SELECT  DISTINCT b.[BookingReference] AS [Booking Reference]
      ,[VisitStartDate] AS [Date of visit]
      ,[FirstName] AS [First name]
      ,[LastName] AS [Last name]
      ,[ReaderTicket] AS [Reader ticket]
	  ,s.Number AS [Seat reserved]
	  ,b.[IsNoFaceCovering] AS [Face Covering Exempt]
	  ,[Comments]
	FROM [KewBooking].[dbo].[Bookings] b
	inner JOIN [dbo].[Seats] s on b.SeatId = s.Id
	left outer JOIN [dbo].[SeatType] st on s.SeatTypeId = st.Id
	inner join [dbo].[BookingStatus] bs on b.BookingStatusId = bs.id
	WHERE b.BookingStatusId = 2 -- completed 
	AND [VisitStartDate] = @dateofvisit
	ORDER BY b.[LastName],[ReaderTicket]

END

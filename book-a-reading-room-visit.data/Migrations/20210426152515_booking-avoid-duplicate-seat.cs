using Microsoft.EntityFrameworkCore.Migrations;

namespace book_a_reading_room_visit.data.Migrations
{
    public partial class bookingavoidduplicateseat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(
				@"================================================
				 SET ANSI_NULLS ON
				GO
				SET QUOTED_IDENTIFIER ON
				GO
				-- =============================================
				-- Author:		Brian O Reilly
				-- Create date: 26 April 2021
				-- Description: Checks for an existing booking
				--              for the same seat and visit
				--              date. If found, rolls back the
				--              insert or update.
				-- =============================================
				CREATE TRIGGER dbo.tr_check_duplicate_seat_booking

				  ON  dbo.Bookings

				  AFTER INSERT, UPDATE
				AS
				BEGIN
				   -- SET NOCOUNT ON added to prevent extra result sets from
				   -- interfering with SELECT statements.

				   SET NOCOUNT ON;

							DECLARE @insertedDate DATETIME;
							DECLARE @insertedSeatId INT;
							DECLARE @insertedDateString varchar(50);
							DECLARE @insertedId INT;

							SELECT @insertedDate = DATEADD(DAY, 0, DATEDIFF(DAY, 0, VisitStartDate)) FROM inserted;
							SELECT @insertedSeatId = SeatId FROM inserted;
							SELECT @insertedId = Id FROM inserted;

							IF EXISTS
							(SELECT id FROM dbo.Bookings WHERE Id<> @insertedId AND DATEADD(DAY, 0, DATEDIFF(DAY, 0, VisitStartDate))= @insertedDate AND SeatId = @insertedSeatId AND BookingTypeId<> 3)
					BEGIN
						SELECT @insertedDateString = convert(varchar, @insertedDate, 23);
							RAISERROR('Seat Id %d already booked for visit date %s', 16, 1, @insertedSeatId, @insertedDateString);
							ROLLBACK;
							END

						END"
				);

		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP TRIGGER dbo.tr_check_duplicate_seat_booking");
		}
    }
}

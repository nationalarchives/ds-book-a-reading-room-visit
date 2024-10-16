using Microsoft.EntityFrameworkCore.Migrations;

namespace book_a_reading_room_visit.data.Migrations
{
    public partial class Bookingavoidduplicateseat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(
				@"
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
					END CATCH;"
				);
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP PROCEDURE dbo.proc_change_booked_seat;");
		}
    }
}

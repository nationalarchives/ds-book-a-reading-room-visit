using Microsoft.EntityFrameworkCore.Migrations;

namespace book_a_reading_room_visit.data.Migrations
{
    public partial class delete_booking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"CREATE PROCEDURE proc_delete_booking_by_id
(
	 @bookingId		int,
	 @deleteCount	int OUTPUT
)
AS
/* Author: Brian O Reilly
** Date: 15th March 2021
** Details: Deletes a booking from the database along with all its document orders.
*/

SET NOCOUNT ON;

BEGIN TRY
	BEGIN TRANSACTION;

	DELETE FROM [dbo].[OrderDocuments] WHERE BookingId = @bookingId;
	DELETE FROM [dbo].[Bookings] WHERE Id = @bookingId;
	SET @deleteCount = @@ROWCOUNT;

	COMMIT TRANSACTION;
END TRY

BEGIN CATCH
	IF XACT_STATE()!=0
	BEGIN
		ROLLBACK TRANSACTION; --only rollback if a transaction is in progress
	END

	-- return the complete original error message to the caller
	DECLARE @ErrorMessage nvarchar(400), @ErrorNumber int, @ErrorSeverity int, @ErrorState int, @ErrorLine int;

	SELECT @ErrorMessage = N'Error %d, Line %d, Message: '+ERROR_MESSAGE(),@ErrorNumber = ERROR_NUMBER(),@ErrorSeverity = ERROR_SEVERITY(),@ErrorState = ERROR_STATE(),@ErrorLine = ERROR_LINE();
	RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState, @ErrorNumber,@ErrorLine);

	RETURN 1;

END CATCH");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP PROCEDURE proc_delete_booking_by_id;");

		}
    }
}

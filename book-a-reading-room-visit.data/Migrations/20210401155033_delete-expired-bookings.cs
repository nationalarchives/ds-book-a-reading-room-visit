using Microsoft.EntityFrameworkCore.Migrations;

namespace book_a_reading_room_visit.data.Migrations
{
    public partial class Deleteexpiredbookings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE [dbo].[proc_delete_expired_bookings]
            (
	            @minute_count INT = 20
            )
            AS
            SET NOCOUNT ON;

            DELETE FROM [dbo].[Bookings]
            WHERE [ReaderTicket] IS NULL AND DATEDIFF(minute, CreatedDate, GETUTCDATE()) > 20; ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE[dbo].[proc_delete_expired_bookings]");
        }
    }
}

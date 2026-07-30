using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace book_a_reading_room_visit.data.Migrations
{
    /// <inheritdoc />
    public partial class LastModifiedEnhancements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "OrderDocuments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "OrderDocuments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Bookings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.Sql(
                @"CREATE TRIGGER [tr_update_booking_last_modified] ON [dbo].[Bookings]
                    AFTER UPDATE
                    AS 
                    BEGIN
	                    SET NOCOUNT ON;
                        UPDATE [dbo].[Bookings] SET [LastModifiedDate] = GETDATE() WHERE Id IN (SELECT Id FROM inserted)
                    END"
                    );

            migrationBuilder.Sql(
                @"CREATE TRIGGER [dbo].[tr_update_order_document_last_modified] ON [dbo].[OrderDocuments]
                    AFTER UPDATE
                AS 
                BEGIN
	                SET NOCOUNT ON;
                    UPDATE [dbo].[OrderDocuments] SET [LastModifiedDate] = GETDATE() WHERE Id IN (SELECT Id FROM inserted)
                END"
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER [dbo].[tr_update_booking_last_modified]");
            migrationBuilder.Sql("DROP TRIGGER [dbo].[tr_update_order_document_last_modified]");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "OrderDocuments");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "OrderDocuments");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Bookings");
        }
    }
}

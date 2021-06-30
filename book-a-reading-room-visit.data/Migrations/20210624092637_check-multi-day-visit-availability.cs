using Microsoft.EntityFrameworkCore.Migrations;

namespace book_a_reading_room_visit.data.Migrations
{
    public partial class checkmultidayvisitavailability : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(
							@"-- ==============================================================
	-- Author:		Brian O Reilly
	-- Create date: 23rd June 2021
	-- Description:	Finds available seats for multi day visits, i.e.
	--				where the seat is not already assigned to another
	--				booking within the date range. This includes seats
	--				assigned for multi-day visits and, optionally,
	--				managerial discretion seats.
	-- ==============================================================
	ALTER PROCEDURE proc_get_available_seats_for_multi_day_visits
(
	@visit_start_date DATETIME,
	@visit_end_date DATETIME,
	@include_managerial_discretion bit = 0

)
AS

BEGIN

SET NOCOUNT ON;

-- Get all the seats

DECLARE @seats TABLE
(
	seat_id int NOT NULL PRIMARY KEY,
	seat_number nvarchar(10) NOT NULL,
	seat_type_id INT NOT NULL
)

IF @include_managerial_discretion = 0
	INSERT INTO @seats (seat_id, seat_number, seat_type_id) SELECT Id, Number, SeatTypeId FROM dbo.Seats WHERE  SeatTypeId = 9;
ELSE
	INSERT INTO @seats (seat_id, seat_number, seat_type_id) SELECT Id, Number, SeatTypeId FROM dbo.Seats WHERE SeatTypeId IN (8, 9);


DECLARE @seats_already_booked TABLE
(
	seat_id INT NOT NULL PRIMARY KEY
)

INSERT INTO @seats_already_booked
	SELECT SeatId FROM dbo.Bookings b INNER JOIN @seats s ON b.SeatId = s.seat_id
	WHERE 
	(
	(b.VisitStartDate >= @visit_start_date AND b.VisitStartDate <= @visit_end_date)
	OR
	(b.VisitEndDate >= @visit_start_date AND b.VisitEndDate <= @visit_end_date)
	)
	AND SeatId IN (SELECT seat_id FROM @seats);

SELECT seat_id as Id, seat_number as [Number], seat_type_id as SeatTypeID FROM @seats WHERE seat_id NOT IN (SELECT seat_id FROM @seats_already_booked);

END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP PROCEDURE dbo.proc_get_available_seats_for_multi_day_visits;");
		}
    }
}

using book_a_reading_room_visit.data;
using book_a_reading_room_visit.domain;
using System.Collections.Generic;

namespace book_a_reading_room_visit.test
{
    public static class TestData
    {
        public static void SeedLookUpData(BookingContext bookingContext)
        {
            bookingContext.BookingStatus.AddRange(new List<BookingStatus>
                {
                    new BookingStatus { Id = 1, Description = "Created" },
                    new BookingStatus { Id = 2, Description = "Submitted" },
                    new BookingStatus { Id = 3, Description = "Cancelled" }
                });

            bookingContext.SeatType.AddRange(new List<SeatType>
                {
                    new SeatType { Id = 1, Description = "Standard reading room seat" },
                    new SeatType { Id = 2, Description = "Standard reading room seat with camera stand" },
                    new SeatType { Id = 3, Description = "Map and large document room seat" },
                    new SeatType { Id = 4, Description = "Bulk document order seat" },
                    new SeatType { Id = 5, Description = "Map and large document room seat with camera stand" },
                    new SeatType { Id = 6, Description = "Bulk document order seat with camera stand" },
                    new SeatType { Id = 7, Description = "Not available" }
                });

            bookingContext.Seats.AddRange(new List<Seat>
                {
                    new Seat { Id = 1, Number = "14H", SeatTypeId = 1 },
                    new Seat { Id = 2, Number = "15D", SeatTypeId = 1 },
                    new Seat { Id = 3, Number = "15H", SeatTypeId = 1 },
                    new Seat { Id = 4, Number = "16D", SeatTypeId = 1 },
                    new Seat { Id = 5, Number = "16H", SeatTypeId = 1 },
                    new Seat { Id = 6, Number = "17D", SeatTypeId = 1 },
                    new Seat { Id = 7, Number = "18D", SeatTypeId = 1 },
                    new Seat { Id = 8, Number = "18H", SeatTypeId = 1 },
                    new Seat { Id = 9, Number = "27B", SeatTypeId = 1 },
                    new Seat { Id = 10, Number = "27F", SeatTypeId = 1 },
                    new Seat { Id = 11, Number = "28B", SeatTypeId = 1 },
                    new Seat { Id = 12, Number = "28F", SeatTypeId = 1 },
                    new Seat { Id = 13, Number = "29D", SeatTypeId = 1 },
                    new Seat { Id = 14, Number = "30D", SeatTypeId = 1 },
                    new Seat { Id = 15, Number = "30G", SeatTypeId = 1 },
                    new Seat { Id = 16, Number = "31B", SeatTypeId = 1 },
                    new Seat { Id = 17, Number = "31F", SeatTypeId = 1 },
                    new Seat { Id = 18, Number = "32B", SeatTypeId = 1 },
                    new Seat { Id = 19, Number = "32F", SeatTypeId = 1 },
                    new Seat { Id = 20, Number = "21B", SeatTypeId = 1 },
                    new Seat { Id = 21, Number = "21F", SeatTypeId = 1 },
                    new Seat { Id = 22, Number = "22H", SeatTypeId = 1 },
                    new Seat { Id = 23, Number = "23B", SeatTypeId = 1 },
                    new Seat { Id = 24, Number = "23F", SeatTypeId = 1 },
                    new Seat { Id = 25, Number = "24B", SeatTypeId = 1 },
                    new Seat { Id = 26, Number = "24F", SeatTypeId = 1 },
                    new Seat { Id = 27, Number = "35B", SeatTypeId = 1 },
                    new Seat { Id = 28, Number = "39B", SeatTypeId = 1 },
                    new Seat { Id = 29, Number = "39F", SeatTypeId = 1 },
                    new Seat { Id = 30, Number = "40B", SeatTypeId = 1 },
                    new Seat { Id = 31, Number = "40F", SeatTypeId = 1 },
                    new Seat { Id = 32, Number = "41B", SeatTypeId = 1 },
                    new Seat { Id = 33, Number = "41F", SeatTypeId = 1 },
                    new Seat { Id = 34, Number = "42B", SeatTypeId = 1 },
                    new Seat { Id = 35, Number = "42F", SeatTypeId = 1 },
                    new Seat { Id = 36, Number = "36B", SeatTypeId = 1 },
                    new Seat { Id = 37, Number = "36F", SeatTypeId = 1 },
                    new Seat { Id = 38, Number = "37D", SeatTypeId = 1 },
                    new Seat { Id = 39, Number = "43B", SeatTypeId = 1 },
                    new Seat { Id = 40, Number = "43F", SeatTypeId = 1 },
                    new Seat { Id = 41, Number = "44F", SeatTypeId = 1 },
                    new Seat { Id = 42, Number = "45F", SeatTypeId = 1 },
                    new Seat { Id = 43, Number = "46H", SeatTypeId = 1 },
                    new Seat { Id = 44, Number = "29G", SeatTypeId = 1 },
                    new Seat { Id = 45, Number = "46D", SeatTypeId = 2 },
                    new Seat { Id = 46, Number = "45B", SeatTypeId = 2 },
                    new Seat { Id = 47, Number = "44B", SeatTypeId = 2 },
                    new Seat { Id = 48, Number = "37G", SeatTypeId = 2 },
                    new Seat { Id = 49, Number = "38H", SeatTypeId = 2 },
                    new Seat { Id = 50, Number = "35F", SeatTypeId = 2 },
                    new Seat { Id = 51, Number = "13A", SeatTypeId = 2 },
                    new Seat { Id = 52, Number = "13C", SeatTypeId = 2 },
                    new Seat { Id = 53, Number = "13E", SeatTypeId = 2 },
                    new Seat { Id = 54, Number = "13G", SeatTypeId = 2 },
                    new Seat { Id = 55, Number = "14E", SeatTypeId = 2 },
                    new Seat { Id = 56, Number = "25B", SeatTypeId = 2 },
                    new Seat { Id = 57, Number = "25D", SeatTypeId = 2 },
                    new Seat { Id = 58, Number = "25F", SeatTypeId = 2 },
                    new Seat { Id = 59, Number = "25H", SeatTypeId = 2 },
                    new Seat { Id = 60, Number = "26B", SeatTypeId = 2 },
                    new Seat { Id = 61, Number = "26D", SeatTypeId = 2 },
                    new Seat { Id = 62, Number = "26F", SeatTypeId = 2 },
                    new Seat { Id = 63, Number = "26H", SeatTypeId = 2 },
                    new Seat { Id = 64, Number = "33B", SeatTypeId = 2 },
                    new Seat { Id = 65, Number = "33D", SeatTypeId = 2 },
                    new Seat { Id = 66, Number = "33F", SeatTypeId = 2 },
                    new Seat { Id = 67, Number = "33H", SeatTypeId = 2 },
                    new Seat { Id = 68, Number = "34B", SeatTypeId = 2 },
                    new Seat { Id = 69, Number = "34D", SeatTypeId = 2 },
                    new Seat { Id = 70, Number = "34F", SeatTypeId = 2 },
                    new Seat { Id = 71, Number = "34H", SeatTypeId = 2 },
                    new Seat { Id = 72, Number = "19A", SeatTypeId = 2 },
                    new Seat { Id = 73, Number = "19C", SeatTypeId = 2 },
                    new Seat { Id = 74, Number = "19E", SeatTypeId = 2 },
                    new Seat { Id = 75, Number = "19G", SeatTypeId = 2 },
                    new Seat { Id = 76, Number = "20A", SeatTypeId = 2 },
                    new Seat { Id = 77, Number = "20C", SeatTypeId = 2 },
                    new Seat { Id = 78, Number = "20E", SeatTypeId = 2 },
                    new Seat { Id = 79, Number = "20G", SeatTypeId = 2 },
                    new Seat { Id = 80, Number = "MR001", SeatTypeId = 3 },
                    new Seat { Id = 81, Number = "MR002", SeatTypeId = 3 },
                    new Seat { Id = 82, Number = "MR003", SeatTypeId = 3 },
                    new Seat { Id = 83, Number = "MR004", SeatTypeId = 3 },
                    new Seat { Id = 84, Number = "MR005", SeatTypeId = 3 },
                    new Seat { Id = 85, Number = "MR006", SeatTypeId = 3 },
                    new Seat { Id = 86, Number = "MR007", SeatTypeId = 3 },
                    new Seat { Id = 87, Number = "MR008", SeatTypeId = 3 },
                    new Seat { Id = 88, Number = "MR009", SeatTypeId = 3 },
                    new Seat { Id = 89, Number = "MR010", SeatTypeId = 3 },
                    new Seat { Id = 90, Number = "MR011", SeatTypeId = 3 },
                    new Seat { Id = 91, Number = "MR012", SeatTypeId = 3 },
                    new Seat { Id = 92, Number = "MR013", SeatTypeId = 3 },
                    new Seat { Id = 93, Number = "MR014", SeatTypeId = 3 },
                    new Seat { Id = 94, Number = "MR015", SeatTypeId = 3 },
                    new Seat { Id = 95, Number = "MR016", SeatTypeId = 3 },
                    new Seat { Id = 96, Number = "MR017", SeatTypeId = 3 },
                    new Seat { Id = 97, Number = "MR018", SeatTypeId = 3 },
                    new Seat { Id = 98, Number = "MR019", SeatTypeId = 3 },
                    new Seat { Id = 99, Number = "MR020", SeatTypeId = 3 },
                    new Seat { Id = 100, Number = "1A", SeatTypeId = 4 },
                    new Seat { Id = 101, Number = "2A", SeatTypeId = 4 }
                });
            bookingContext.SaveChanges();
        }
    }
}

using book_a_reading_room_visit.domain;
using Microsoft.EntityFrameworkCore;

namespace book_a_reading_room_visit.data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingStatus>().HasData(
                new BookingStatus { Id = 1, Description = "Created" },
                new BookingStatus { Id = 2, Description = "Submitted" },
                new BookingStatus { Id = 3, Description = "Cancelled" }
            );

            modelBuilder.Entity<SeatType>().HasData(
                new SeatType { Id = 1, Description = "Standard reading room seat" },
                new SeatType { Id = 2, Description = "Standard reading room seat with camera stand" },
                new SeatType { Id = 3, Description = "Map and large document room seat" },
                new SeatType { Id = 4, Description = "Bulk document order seat" },
                new SeatType { Id = 5, Description = "Map and large document room seat with camera stand" },
                new SeatType { Id = 6, Description = "Bulk document order seat with camera stand" },
                new SeatType { Id = 7, Description = "Not available" }
            );
        }
    }
}

using book_a_reading_room_visit.api.Models;
using book_a_reading_room_visit.domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Service
{
    public interface IBookingService
    {
        Task<string> CreateBookingAsync(BookingModel bookingModel);
        Task<List<Booking>> GetBookingSummaryAsync(BookingSearchModel bookingSearchModel);
        Task<Booking> GetBookingById(int bookingId);
    }
}

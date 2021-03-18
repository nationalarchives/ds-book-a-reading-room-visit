using book_a_reading_room_visit.api.Models;
using book_a_reading_room_visit.domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Service
{
    public interface IBookingService
    {
        Task<BookingResponseModel> CreateBookingAsync(BookingModel bookingModel);
        Task<BookingResponseModel> ConfirmBookingAsync(BookingModel bookingModel);
        Task<BookingResponseModel> UpdateSeatBookingAsync(int bookingId, int newSeatId);
        Task<Booking> GetBookingByIdAsync(int bookingId);
        Task<List<Booking>> BookingSearchAsync(BookingSearchModel bookingSearchModel);
    }
}

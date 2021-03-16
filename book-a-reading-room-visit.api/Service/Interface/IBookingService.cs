using book_a_reading_room_visit.api.Models;
using book_a_reading_room_visit.domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Service
{
    public interface IBookingService
    {
        Task<BookingResponseModel> CreateBookingAsync(BookingModel bookingModel);
        Task<List<Booking>> BookingSearchAsync(BookingSearchModel bookingSearchModel);
        Task<BookingResponseModel> UpdateReaderTicketAsync(BookingModel bookingModel);
        Task<Booking> GetBookingByReference(string bookingReference);
        Task<Booking> GetBookingById(int bookingId);
    }
}

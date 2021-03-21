using book_a_reading_room_visit.model;
using book_a_reading_room_visit.web.Models;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.web.Service
{
    public interface IBookingService
    {
        Task<BookingResponseModel> CreateBookingAsync(BookingModel bookingModel);
        Task<BookingResponseModel> ReserveSpaceAsync(BookingModel bookingModel);
        Task<BookingModel> GetBookingAsync(int readerTicket, string bookingReference);
        Task DeleteBookingAsync(string bookingReference);
        Task CancelBookingAsync(BookingCancellationModel cancelViewModel);
    }
}

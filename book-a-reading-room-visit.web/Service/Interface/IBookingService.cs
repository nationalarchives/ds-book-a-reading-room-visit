using book_a_reading_room_visit.web.Models;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.web.Service
{
    public interface IBookingService
    {
        Task<BookingResponseModel> CreateBookingAsync(BookingViewModel bookingViewModel);
        Task<BookingResponseModel> ReserveSpaceAsync(BookingViewModel bookingViewModel);
    }
}

using book_a_reading_room_visit.api.Models;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Service
{
    public interface IBookingService
    {
        Task<string> CreateBookingAsync(BookingModel bookingModel);
        Task<IList<Booking>> GetBookingSummaryAsync(BookingSearchModel bs);
    }
}

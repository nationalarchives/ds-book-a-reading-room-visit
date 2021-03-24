using book_a_reading_room_visit.domain;
using book_a_reading_room_visit.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Service
{
    public interface IBookingService
    {
        Task<BookingResponseModel> CreateBookingAsync(BookingModel bookingModel);
        Task<BookingResponseModel> ConfirmBookingAsync(BookingModel bookingModel);
        Task<BookingResponseModel> UpdateSeatBookingAsync(int bookingId, int newSeatId, string comment, string updatedBy);
        Task<BookingResponseModel> CancelBookingAsync(BookingCancellationModel bookingCancellationModel);
        Task<bool> UpdateBookingCommentsAsync(BookingCommentsModel bookingCommentsModel);
        Task<BookingModel> GetBookingByIdAsync(int bookingId);
        Task<BookingModel> GetBookingByReferenceAsync(int readerTicket, string bookingReference);
        Task<List<BookingModel>> BookingSearchAsync(BookingSearchModel bookingSearchModel);
        Task<bool> DeleteBookingAsync(string bookingReference);
    }
}

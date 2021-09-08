using book_a_reading_room_visit.model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Service
{
    public interface IBookingService
    {
        Task<BookingResponseModel> CreateBookingAsync(BookingModel bookingModel);
        Task<BookingResponseModel> CreateMultiDayBookingAsync(BookingModelMultiDay bookingModelMultiDay);
        Task<BookingResponseModel> ConfirmBookingAsync(BookingModel bookingModel);
        Task<BookingResponseModel> UpsertDocumentsAsync(BookingModel bookingModel);
        Task<BookingResponseModel> UpdateSeatBookingAsync(int bookingId, int newSeatId, string comment, string updatedBy);
        Task<BookingResponseModel> CancelBookingAsync(BookingCancellationModel bookingCancellationModel);
        Task<bool> UpdateBookingCommentsAsync(BookingCommentsModel bookingCommentsModel);
        Task<bool> ToggleNoShowAsync(int bookingId);
        Task<BookingModel> GetBookingByIdAsync(int bookingId);
        Task<BookingModel> GetBookingByReferenceAsync(string bookingReference);
        Task<BookingModel> GetBookingByReaderTicketAndReferenceAsync(int readerTicket, string bookingReference);
        Task<List<BookingModel>> BookingSearchAsync(BookingSearchModel bookingSearchModel);
        Task<bool> DeleteBookingAsync(string bookingReference);
        Task<ValidationResult> GetReaderTicketEligibilityAsync(int readerTicket, DateTime visitDate);
        Task<int> SubmitBookingAsync(DateTime completeBy);
        Task<int> SendBookingConfirmationEmailsAsync(DateTime completeBy);
        Task<int> SendReminderNotificationEmailsAsync(DateTime completeBy);
        Task<int> SendPostVisitSurveyEmailsAsync(DateTime visitEndDate);
    }
}

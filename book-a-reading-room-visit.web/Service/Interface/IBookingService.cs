﻿using book_a_reading_room_visit.model;
using System;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.web.Service
{
    public interface IBookingService
    {
        Task<BookingResponseModel> CreateBookingAsync(BookingModel bookingModel);
        Task<BookingResponseModel> ReserveSpaceAsync(BookingModel bookingModel);
        Task<BookingResponseModel> UpsertDocumentAsync(BookingModel bookingModel);
        Task<BookingModel> GetBookingAsync(string bookingReference);
        Task<BookingModel> GetBookingAsync(int readerTicket, string bookingReference);
        Task<ValidationResult> GetReaderTicketEligibleAsync(int readerTicket, DateTime visitDate);
        Task DeleteBookingAsync(string bookingReference);
        Task CancelBookingAsync(BookingCancellationModel cancelViewModel);
    }
}

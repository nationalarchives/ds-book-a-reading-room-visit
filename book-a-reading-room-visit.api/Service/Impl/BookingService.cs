using book_a_reading_room_visit.api.Helper;
using book_a_reading_room_visit.api.Models;
using book_a_reading_room_visit.data;
using book_a_reading_room_visit.domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Service
{
    public class BookingService : IBookingService
    {
        private readonly BookingContext _context;
        private const string Modified_By = "system";

        public BookingService(BookingContext context)
        {
            _context = context;
        }

        public async Task<List<Booking>> BookingSearchAsync(BookingSearchModel bookingSearchModel)
        {
            DateTime? dateComponent = null;

            if (bookingSearchModel.Date.HasValue)
            {
                dateComponent = bookingSearchModel.Date.Value.Date;
            }

            var bookings = await _context.Bookings.AsNoTracking<Booking>().Where(b =>
                                            (bookingSearchModel.BookingReference == null || bookingSearchModel.BookingReference  == b.BookingReference) &&
                                            (bookingSearchModel.ReadersTicket == null || bookingSearchModel.ReadersTicket == b.ReaderTicket) &&
                                            (dateComponent == null || dateComponent == b.VisitStartDate.Date)
                                            ).Include(b => b.BookingStatus).Include(b => b.Seat).ThenInclude(s => s.SeatType)
                                            .TagWith<Booking>("Search of Bookings").ToListAsync();
            
            return bookings;
        }

        public async Task<BookingResponseModel> CreateBookingAsync(BookingModel bookingModel)
        {
            var response = new BookingResponseModel { IsSuccess = true };

            var seatAvailable = await (from seat in _context.Set<Seat>().Where(s => (SeatTypes)s.SeatTypeId == bookingModel.SeatType)
                                       join booking in _context.Set<Booking>().Where(b => b.VisitStartDate == bookingModel.BookingStartDate)
                                       on seat.Id equals booking.SeatId into lj
                                       from subseat in lj.DefaultIfEmpty()
                                       where subseat == null
                                       select seat).FirstOrDefaultAsync();

            if (seatAvailable?.Id == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = $"There is no seat available for the given seat type {bookingModel.SeatType.ToString()} and date {bookingModel.BookingStartDate:dd-MM-yyyy}";
                return response;
            }

            var bookingId = (await _context.Set<Booking>().OrderByDescending(b => b.Id).FirstOrDefaultAsync())?.Id ?? 0 + 1;

            response.BookingReference = IdGenerator.GenerateBookingReference(bookingId);

            await _context.Set<Booking>().AddAsync(new Booking
            {
                CreatedDate = DateTime.Now,
                BookingReference = response.BookingReference,
                BookingTypeId = (int)bookingModel.BookingType,
                IsAcceptTsAndCs = false,
                IsAcceptCovidCharter = false,
                IsNoShow = false,
                SeatId = seatAvailable.Id,
                BookingStatusId = (int)BookingStatuses.Created,
                VisitStartDate = bookingModel.BookingStartDate,
                VisitEndDate = bookingModel.BookingEndDate,
                LastModifiedBy = Modified_By
            });
            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<BookingResponseModel> UpdateReaderTicketAsync(BookingModel bookingModel)
        {
            var response = new BookingResponseModel { IsSuccess = true, BookingReference = bookingModel.BookingReference };

            var booking = await _context.Set<Booking>().FirstOrDefaultAsync(b => b.BookingReference == bookingModel.BookingReference);

            if (booking == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = $"There is no booking found for the booking reference {bookingModel.BookingReference}";
                return response;
            }

            booking.ReaderTicket = bookingModel.ReadingTicket;
            booking.Email = bookingModel.Email;
            booking.FirstName = bookingModel.FirstName;
            booking.LastName = bookingModel.LastName;
            booking.Phone = bookingModel.Phone;
            booking.IsAcceptTsAndCs = bookingModel.AcceptTsAndCs;
            booking.IsAcceptCovidCharter = bookingModel.AcceptCovidCharter;

            _context.Entry(booking).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return response;
        }
    }
}

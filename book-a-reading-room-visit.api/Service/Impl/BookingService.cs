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

        public BookingService(BookingContext context)
        {
            _context = context;
        }

        public async Task<List<Booking>> GetBookingSummaryAsync(BookingSearchModel bookingSearchModel)
        {
            DateTime? dateComponent = null;

            if(bookingSearchModel.Date.HasValue)
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

        public async Task<string> CreateBookingAsync(BookingModel bookingModel)
        {
            var seatId = await (from seat in _context.Set<Seat>().Where(s => (SeatTypes)s.SeatTypeId == bookingModel.SeatType)
                                join booking in _context.Set<Booking>().Where(b => b.VisitStartDate == bookingModel.BookingStartDate)
                                on seat.Id equals booking.SeatId into lj
                                from subseat in lj.DefaultIfEmpty()
                                select seat.Id).FirstOrDefaultAsync();

            var bookingId = (await _context.Set<Booking>().OrderByDescending(b => b.Id).FirstOrDefaultAsync())?.Id ?? 0 + 1;

            var bookingReference = IdGenerator.GenerateBookingReference(bookingId);

            await _context.Set<Booking>().AddAsync(new Booking
                                                { 
                                                    CreatedDate = DateTime.Now,
                                                    BookingReference = bookingReference,
                                                    IsAcceptTsAndCs = false,
                                                    IsAcceptCovidCharter = false,
                                                    IsNoShow = false,
                                                    SeatId = seatId,
                                                    BookingStatusId = (int)BookingStatuses.Created,
                                                    VisitStartDate = bookingModel.BookingStartDate,
                                                    VisitEndDate = bookingModel.BookingEndDate,
                                                });
            await _context.SaveChangesAsync();
            return bookingReference;
        }

        public async Task<Booking> GetBookingByReference(string bookingReference)
        {
            var booking = await _context.Bookings.AsNoTracking<Booking>()
                .Include(b => b.BookingStatus)
                .Include(b => b.Seat).ThenInclude(s => s.SeatType)
                .Include(b => b.OrderDocuments)
                .TagWith<Booking>("Find Booking by ID")
                .FirstOrDefaultAsync(b => b.BookingReference == bookingReference);

            return booking;
        }

        public async Task<Booking> GetBookingById(int bookingId)
        {
            var booking = await _context.Bookings.AsNoTracking<Booking>()
                .Include(b => b.BookingStatus)
                .Include(b => b.Seat).ThenInclude(s => s.SeatType)
                .Include(b => b.OrderDocuments)
                .TagWith<Booking>("Find Booking by ID")
                .FirstOrDefaultAsync(b => b.Id == bookingId);

            return booking;
        }
    }
}

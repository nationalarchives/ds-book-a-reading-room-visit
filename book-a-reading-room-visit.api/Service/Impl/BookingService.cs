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
                .TagWith<Booking>("Find Booking by Reference")
                .FirstOrDefaultAsync(b => b.BookingReference == bookingReference);

            var bookingToReturn = GetSerialisedBooking(booking);
            return bookingToReturn;
        }

        public async Task<Booking> GetBookingById(int bookingId)
        {
            var booking = await _context.Bookings.AsNoTracking<Booking>()
                .Include(b => b.BookingStatus)
                .Include(b => b.Seat).ThenInclude(s => s.SeatType)
                .Include(b => b.OrderDocuments)
                .TagWith<Booking>("Find Booking by ID")
                .FirstOrDefaultAsync(b => b.Id == bookingId);

            var bookingToReturn = GetSerialisedBooking(booking);
            return bookingToReturn;
        }

        /// <summary>
        /// Creates a booking with the circular reference from OrderDocument back to booking removed.
        /// This works around an issue with the default Json serializer.
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        private Booking GetSerialisedBooking(Booking booking)
        {
            var result = new Booking()
            {
                Id = booking.Id,
                BookingReference = booking.BookingReference,
                BookingStatus = booking.BookingStatus,
                BookingStatusId = booking.BookingStatusId,
                AdditionalRequirements = booking.AdditionalRequirements,
                Comments = booking.Comments,
                BookingType = booking.BookingType,
                BookingTypeId = booking.BookingTypeId,
                Email = booking.Email,
                Phone = booking.Phone,
                FirstName = booking.FirstName,
                LastName = booking.LastName,
                ReaderTicket = booking.ReaderTicket,
                VisitStartDate = booking.VisitStartDate,
                VisitEndDate = booking.VisitEndDate,
                SeatId = booking.SeatId,
                Seat = booking.Seat,
                IsAcceptCovidCharter = booking.IsAcceptCovidCharter,
                IsAcceptTsAndCs = booking.IsAcceptTsAndCs,
                IsNoShow = booking.IsNoShow,
                CreatedDate = booking.CreatedDate,
                LastModifiedBy = booking.LastModifiedBy,
                OrderDocuments = new List<OrderDocument>()
            };

            foreach (OrderDocument o in booking.OrderDocuments)
            {
                result.OrderDocuments.Add(new OrderDocument()
                {
                    ClassNumber = o.ClassNumber,
                    DocumentReference = o.DocumentReference,
                    Id = o.Id,
                    IsReserve = o.IsReserve,
                    ItemReference = o.ItemReference,
                    LetterCode = o.LetterCode,
                    PieceId = o.PieceId,
                    PieceReference = o.PieceReference,
                    Site = o.Site,
                    SubClassNumber = o.SubClassNumber
                });
            }

            return result;
        }
    }
}

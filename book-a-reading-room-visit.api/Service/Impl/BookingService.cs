using book_a_reading_room_visit.api.Helper;
using book_a_reading_room_visit.data;
using book_a_reading_room_visit.domain;
using book_a_reading_room_visit.model;
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
        private readonly IWorkingDayService _workingDayService;
        private const string Modified_By = "system";

        public BookingService(BookingContext context, IWorkingDayService workingDayService)
        {
            _context = context;
            _workingDayService = workingDayService;
        }

        public async Task<BookingResponseModel> CreateBookingAsync(BookingModel bookingModel)
        {
            var response = new BookingResponseModel { IsSuccess = true };

            var seatAvailable = await (from seat in _context.Set<Seat>().Where(s => (SeatTypes)s.SeatTypeId == bookingModel.SeatType)
                                       join booking in _context.Set<Booking>().Where(b => b.VisitStartDate == bookingModel.VisitStartDate && b.BookingStatusId != (int)BookingStatuses.Cancelled)
                                       on seat.Id equals booking.SeatId into lj
                                       from subseat in lj.DefaultIfEmpty()
                                       where subseat == null
                                       select seat).FirstOrDefaultAsync();

            if (seatAvailable?.Id == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = $"There is no seat available for the given seat type {bookingModel.SeatType.ToString()} and date {bookingModel.VisitStartDate:dd-MM-yyyy}";
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
                IsNoFaceCovering = false,
                IsNoShow = false,
                SeatId = seatAvailable.Id,
                BookingStatusId = (int)BookingStatuses.Created,
                VisitStartDate = bookingModel.VisitStartDate,
                VisitEndDate = bookingModel.VisitEndDate,
                LastModifiedBy = Modified_By
            });
            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<BookingResponseModel> ConfirmBookingAsync(BookingModel bookingModel)
        {
            var response = new BookingResponseModel { IsSuccess = true, BookingReference = bookingModel.BookingReference };

            var booking = await _context.Set<Booking>().Include(b => b.Seat).FirstOrDefaultAsync(b => b.BookingReference == bookingModel.BookingReference);

            if (booking == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = $"There is no booking found for the booking reference {bookingModel.BookingReference}";
                return response;
            }
            response.CompleteByDate = await _workingDayService.GetCompleteByDateAsync(booking.VisitStartDate);
            response.SeatNumber = booking.Seat.Number;

            booking.CompleteByDate = response.CompleteByDate;
            booking.ReaderTicket = bookingModel.ReaderTicket;
            booking.FirstName = bookingModel.FirstName;
            booking.LastName = bookingModel.LastName;
            booking.Email = bookingModel.Email;
            booking.Phone = bookingModel.Phone;
            booking.IsAcceptTsAndCs = bookingModel.IsAcceptTsAndCs;
            booking.IsAcceptCovidCharter = bookingModel.IsAcceptCovidCharter;
            booking.IsNoFaceCovering = bookingModel.IsNoFaceCovering;

            _context.Entry(booking).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<BookingResponseModel> UpdateSeatBookingAsync(int bookingId, int newSeatId)
        {
            var response = new BookingResponseModel { IsSuccess = true };

            var booking = await _context.Set<Booking>().FirstOrDefaultAsync(b => b.Id == bookingId);

            if (booking == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = $"There is no booking found for the booking id {bookingId}";
                return response;
            }

            bool seatTaken = await _context.Set<Booking>().Where(b => b.VisitStartDate == booking.VisitStartDate && b.SeatId == newSeatId && b.Id != bookingId).AnyAsync();

            if (seatTaken)
            {
                response.IsSuccess = false;
                response.ErrorMessage = $"The requested seat is not available on {booking.VisitStartDate:dd-MM-yyyy}";
                return response;
            }

            _context.Attach(booking);
            booking.SeatId = newSeatId;
            _context.SaveChanges();

            return response;

        }

        public async Task<BookingResponseModel> CancelBookingAsync(BookingCancellationModel bookingCancellationModel)
        {
            var response = new BookingResponseModel { IsSuccess = true };

            var booking = bookingCancellationModel.BookingId > 0 ? await _context.Set<Booking>().FirstOrDefaultAsync(b => b.Id == bookingCancellationModel.BookingId)
                                                                 : await _context.Set<Booking>().FirstOrDefaultAsync(b => b.BookingReference == bookingCancellationModel.BookingReference && 
                                                                                                                          b.ReaderTicket == bookingCancellationModel.ReaderTicket);

            if (booking == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = $"There is no booking found for the booking id {bookingCancellationModel.BookingId}";
                return response;
            }

            booking.BookingStatusId = (int)BookingStatuses.Cancelled;
            booking.LastModifiedBy = bookingCancellationModel.CancelledBy;
            _context.Entry(booking).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<BookingModel> GetBookingByIdAsync(int bookingId)
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

        public async Task<BookingModel> GetBookingByReferenceAsync(int readerTicket, string bookingReference)
        {
            var booking = await _context.Bookings
                .Include(b => b.Seat)
                .Include(b => b.OrderDocuments)
                .FirstOrDefaultAsync(b => b.ReaderTicket == readerTicket && b.BookingReference == bookingReference);

            if (booking == null)
            {
                return null;
            }
            var bookingToReturn = GetSerialisedBooking(booking);
            return bookingToReturn;
        }

        public async Task<List<Booking>> BookingSearchAsync(BookingSearchModel bookingSearchModel)
        {
            DateTime? dateComponent = null;

            if (bookingSearchModel.Date.HasValue)
            {
                dateComponent = bookingSearchModel.Date.Value.Date;
            }

            var bookings = await _context.Bookings.AsNoTracking<Booking>().Where(b =>
                                            (bookingSearchModel.BookingReference == null || bookingSearchModel.BookingReference == b.BookingReference) &&
                                            (bookingSearchModel.ReadersTicket == null || bookingSearchModel.ReadersTicket == b.ReaderTicket) &&
                                            (dateComponent == null || dateComponent == b.VisitStartDate.Date)
                                            ).Include(b => b.BookingStatus).Include(b => b.Seat).ThenInclude(s => s.SeatType)
                                            .TagWith<Booking>("Search of Bookings").ToListAsync();

            return bookings;
        }

        /// <summary>
        /// Creates a booking with the circular reference from OrderDocument back to booking removed.
        /// This works around an issue with the default Json serializer.
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        private BookingModel GetSerialisedBooking(Booking booking)
        {
            var result = new BookingModel()
            {
                Id = booking.Id,
                BookingReference = booking.BookingReference,
                BookingStatus = (BookingStatuses)booking.BookingStatusId,
                AdditionalRequirements = booking.AdditionalRequirements,
                Comments = booking.Comments,
                BookingType = (BookingTypes)booking.BookingTypeId,
                Email = booking.Email,
                Phone = booking.Phone,
                FirstName = booking.FirstName,
                LastName = booking.LastName,
                ReaderTicket = booking.ReaderTicket,
                VisitStartDate = booking.VisitStartDate,
                VisitEndDate = booking.VisitEndDate,
                CompleteByDate = booking.CompleteByDate ?? default(DateTime),
                SeatType = (SeatTypes)booking.Seat.SeatTypeId,
                SeatNumber = booking.Seat.Number,
                IsAcceptCovidCharter = booking.IsAcceptCovidCharter,
                IsAcceptTsAndCs = booking.IsAcceptTsAndCs,
                IsNoShow = booking.IsNoShow,
                IsNoFaceCovering = booking.IsNoFaceCovering,
                CreatedDate = booking.CreatedDate,
                LastModifiedBy = booking.LastModifiedBy,
                OrderDocuments = new List<OrderDocumentModel>()
            };

            foreach (var document in booking.OrderDocuments)
            {
                result.OrderDocuments.Add(new OrderDocumentModel()
                {
                    ClassNumber = document.ClassNumber,
                    DocumentReference = document.DocumentReference,
                    Id = document.Id,
                    IsReserve = document.IsReserve,
                    ItemReference = document.ItemReference,
                    LetterCode = document.LetterCode,
                    PieceId = document.PieceId,
                    PieceReference = document.PieceReference,
                    Site = document.Site,
                    SubClassNumber = document.SubClassNumber
                });
            }

            return result;
        }

        public async Task<bool> DeleteBookingAsync(string bookingReference)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingReference == bookingReference);
            if (booking == null)
            {
                return false;
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

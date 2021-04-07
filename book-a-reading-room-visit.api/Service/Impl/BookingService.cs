using book_a_reading_room_visit.api.Helper;
using book_a_reading_room_visit.data;
using book_a_reading_room_visit.domain;
using book_a_reading_room_visit.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private const string Modified_By = "system";

        public BookingService(BookingContext context, IWorkingDayService workingDayService, IEmailService emailService, IConfiguration configuration)
        {
            _context = context;
            _workingDayService = workingDayService;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<BookingResponseModel> CreateBookingAsync(BookingModel bookingModel)
        {
            var response = new BookingResponseModel { IsSuccess = true };
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var seatAvailable = await (from seat in _context.Set<Seat>().Where(s => (SeatTypes)s.SeatTypeId == bookingModel.SeatType)
                                           join booking in _context.Set<Booking>().Where(b => b.VisitStartDate == bookingModel.VisitStartDate && b.BookingStatusId != (int)BookingStatuses.Cancelled)
                                           on seat.Id equals booking.SeatId into lj
                                           from subseat in lj.DefaultIfEmpty()
                                           where subseat == null
                                           select seat).FirstOrDefaultAsync();

                if (seatAvailable?.Id == null)
                {
                    await transaction.RollbackAsync();
                    response.IsSuccess = false;
                    response.ErrorMessage = $"There is no seat available for the given seat type {bookingModel.SeatType} on the date {bookingModel.VisitStartDate:dd-MM-yyyy}";
                    return response;
                }

                var bookingId = (await _context.Set<Booking>().OrderByDescending(b => b.Id).FirstOrDefaultAsync())?.Id ?? 0 + 1;

                response.BookingReference = IdGenerator.GenerateBookingReference(bookingId);
                response.CreatedDate = DateTime.UtcNow;

                await _context.Set<Booking>().AddAsync(new Booking
                {
                    CreatedDate = response.CreatedDate,
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
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                response.IsSuccess = false;
                response.ErrorMessage = $"Error reserving the given seat type {bookingModel.SeatType} on the date {bookingModel.VisitStartDate:dd-MM-yyyy}";
            }
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

            _context.Attach(booking);
            booking.CompleteByDate = response.CompleteByDate;
            booking.ReaderTicket = bookingModel.ReaderTicket;
            booking.FirstName = bookingModel.FirstName;
            booking.LastName = bookingModel.LastName;
            booking.Email = bookingModel.Email;
            booking.Phone = bookingModel.Phone;
            booking.IsAcceptTsAndCs = bookingModel.IsAcceptTsAndCs;
            booking.IsAcceptCovidCharter = bookingModel.IsAcceptCovidCharter;
            booking.IsNoFaceCovering = bookingModel.IsNoFaceCovering;

            await _context.SaveChangesAsync();

            if (!string.IsNullOrWhiteSpace(bookingModel.Email))
            {
                bookingModel.CompleteByDate = response.CompleteByDate;
                bookingModel.SeatNumber = response.SeatNumber;
                bookingModel.VisitStartDate = booking.VisitStartDate;
                bookingModel.OrderDocuments = new List<OrderDocumentModel>();
                await _emailService.SendEmailAsync(EmailType.ReservationConfirmation, bookingModel.Email, bookingModel);
            }
            return response;
        }

        public async Task<BookingResponseModel> UpsertDocumentsAsync(BookingModel bookingModel)
        {
            var response = new BookingResponseModel { IsSuccess = true, BookingReference = bookingModel.BookingReference };
            var booking = await _context.Set<Booking>().FirstOrDefaultAsync(b => b.BookingReference == bookingModel.BookingReference);

            if (booking == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = $"There is no booking found for the booking reference {bookingModel.BookingReference}";
                return response;
            }

            _context.Attach(booking);
            booking.AdditionalRequirements = bookingModel.AdditionalRequirements;

            var documents = await _context.Set<OrderDocument>().Where(d => d.BookingId == booking.Id).ToListAsync();
            _context.Set<OrderDocument>().RemoveRange(documents);

            var orderDocuments = (from document in bookingModel.OrderDocuments
                                  select new OrderDocument
                                  {
                                      DocumentReference = document.DocumentReference,
                                      Description = document.Description,
                                      BookingId = booking.Id,
                                      LetterCode = document.LetterCode,
                                      ClassNumber = document.ClassNumber,
                                      PieceId = document.PieceId,
                                      PieceReference = document.PieceReference,
                                      SubClassNumber = document.SubClassNumber,
                                      ItemReference = document.ItemReference,
                                      Site = document.Site,
                                      IsReserve = document.IsReserve
                                  }).ToList();

            await _context.Set<OrderDocument>().AddRangeAsync(orderDocuments);

            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<BookingResponseModel> UpdateSeatBookingAsync(int bookingId, int newSeatId, string comment, string updatedBy)
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
            if (!String.IsNullOrWhiteSpace(booking.Comments))
            {
                booking.Comments += " " + comment;
            }
            else
            {
                booking.Comments = comment;
            }
            booking.SeatId = newSeatId;
            booking.LastModifiedBy = updatedBy;

            await _context.SaveChangesAsync();

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

            _context.Attach(booking);
            booking.BookingStatusId = (int)BookingStatuses.Cancelled;
            booking.LastModifiedBy = bookingCancellationModel.CancelledBy;
            await _context.SaveChangesAsync();

            if (!string.IsNullOrWhiteSpace(booking.Email))
            {
                var cancelledBooking = await _context.Bookings.AsNoTracking<Booking>().Include(s => s.Seat).FirstOrDefaultAsync(b => b.Id == booking.Id);
                var bookingModel = GetSerialisedBooking(cancelledBooking);
                await _emailService.SendEmailAsync(EmailType.BookingCancellation, bookingModel.Email, bookingModel);
            }

            return response;
        }

        public async Task<BookingModel> GetBookingByIdAsync(int bookingId)
        {
            var booking = await _context.Bookings.AsNoTracking<Booking>()
                .Include(b => b.BookingStatus)
                .Include(b => b.BookingType)
                .Include(b => b.Seat).ThenInclude(s => s.SeatType)
                .Include(b => b.OrderDocuments)
                .TagWith<Booking>("Find Booking by ID")
                .FirstOrDefaultAsync(b => b.Id == bookingId);

            if (booking != null)
            {
                var bookingToReturn = GetSerialisedBooking(booking);
                return bookingToReturn;
            }
            else
            {
                return null;
            }
        }

        public async Task<BookingModel> GetBookingByReferenceAsync(string bookingReference)
        {
            var booking = await _context.Bookings
                .Include(b => b.Seat)
                .FirstOrDefaultAsync(b => b.BookingReference == bookingReference);

            if (booking == null)
            {
                return null;
            }
            var bookingToReturn = GetSerialisedBooking(booking);
            return bookingToReturn;
        }

        public async Task<BookingModel> GetBookingByReaderTicketAndReferenceAsync(int readerTicket, string bookingReference)
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

        public async Task<List<BookingModel>> BookingSearchAsync(BookingSearchModel bookingSearchModel)
        {
            DateTime? dateComponent = null;

            if (bookingSearchModel.Date.HasValue)
            {
                dateComponent = bookingSearchModel.Date.Value.Date;
            }

            var bookings = await _context.Bookings.AsNoTracking<Booking>().Where(b =>
                                            (bookingSearchModel.BookingReference == null || bookingSearchModel.BookingReference == b.BookingReference) &&
                                            (bookingSearchModel.ReadersTicket == null || bookingSearchModel.ReadersTicket == b.ReaderTicket) &&
                                            (dateComponent == null || dateComponent == b.VisitStartDate.Date) &&
                                            (String.IsNullOrEmpty(bookingSearchModel.LastName) || b.LastName.Contains(bookingSearchModel.LastName))
                                            ).Include(b => b.BookingType).Include(b => b.BookingStatus)
                                            .Include(b => b.Seat).ThenInclude(s => s.SeatType)
                                            .Include(b => b.OrderDocuments)
                                            .TagWith<Booking>("Search of Bookings").ToListAsync();

            var bookingModels = bookings.Select(b => new BookingModel()
            {
                Id = b.Id,
                BookingReference = b.BookingReference,
                BookingType = (BookingTypes)b.BookingType.Id,
                BookingStatus = (BookingStatuses)b.BookingStatusId,
                FirstName = b.FirstName,
                LastName = b.LastName,
                Email = b.Email,
                Phone = b.Phone,
                CreatedDate = b.CreatedDate,
                CompleteByDate = b.CompleteByDate,
                AdditionalRequirements = b.AdditionalRequirements,
                Comments = b.Comments,
                IsAcceptCovidCharter = b.IsAcceptCovidCharter,
                IsAcceptTsAndCs = b.IsAcceptTsAndCs,
                IsNoFaceCovering = b.IsNoFaceCovering,
                IsNoShow = b.IsNoShow,
                ReaderTicket = b.ReaderTicket,
                SeatId = b.SeatId,
                SeatNumber = b.Seat.Number,
                SeatType = (SeatTypes)b.Seat.SeatTypeId,
                SeatTypeDescription = b.Seat.SeatType.Description,
                VisitStartDate = b.VisitStartDate,
                VisitEndDate = b.VisitEndDate,
                LastModifiedBy = b.LastModifiedBy,
                OrderDocuments = AddOrderDocuments(b)
            });

            return bookingModels.ToList();

            List<OrderDocumentModel> AddOrderDocuments(Booking b)
            {
                var orderDocumentList = new List<OrderDocumentModel>();

                foreach (Booking booking in bookings)
                {
                    foreach (OrderDocument od in booking.OrderDocuments)
                    {
                        orderDocumentList.Add(new OrderDocumentModel()
                        {
                            Id = od.Id,
                            DocumentReference = od.DocumentReference,
                            Description = od.Description,
                            PieceId = od.PieceId,
                            PieceReference = od.PieceReference,
                            ItemReference = od.ItemReference,
                            ClassNumber = od.ClassNumber,
                            SubClassNumber = od.SubClassNumber,
                            LetterCode = od.LetterCode,
                            IsReserve = od.IsReserve,
                            Site = od.Site
                        });
                    }
                }

                return orderDocumentList;
            }
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
                BookingTypeDescription = booking.BookingType?.Description,
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

            if (booking.OrderDocuments != null)
            {
                foreach (var document in booking.OrderDocuments)
                {
                    result.OrderDocuments.Add(new OrderDocumentModel()
                    {
                        ClassNumber = document.ClassNumber,
                        DocumentReference = document.DocumentReference,
                        Description = document.Description,
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

        public async Task<bool> UpdateBookingCommentsAsync(BookingCommentsModel bookingCommentsModel)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == bookingCommentsModel.BookingId);
            if (booking == null)
            {
                return false;
            }

            _context.Attach(booking);
            booking.Comments = bookingCommentsModel.Comments;
            booking.LastModifiedBy = bookingCommentsModel.UpdatedBy;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsOrderLimitExceedAsync(int readerTicket, DateTime visitDate)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.ReaderTicket == readerTicket && b.VisitStartDate == visitDate);

            if (booking != null)
            {
                return true;
            }
            var orderLimit = int.Parse(_configuration.GetSection("BookingTimeLine:OrderLimitPerReaderTicket").Value);
            var orderLimitDuration = int.Parse(_configuration.GetSection("BookingTimeLine:OrderLimitDuration").Value);
            var endDate = DateTime.Today.AddDays(orderLimitDuration);

            var bookings = await _context.Bookings.CountAsync(b => b.ReaderTicket == readerTicket && b.VisitStartDate >= DateTime.Today && b.VisitStartDate <= endDate);

            return bookings > orderLimit;
        }
    }
}

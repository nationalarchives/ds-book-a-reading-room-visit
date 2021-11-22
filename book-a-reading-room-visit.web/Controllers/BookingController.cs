using book_a_reading_room_visit.model;
using book_a_reading_room_visit.web.Helper;
using book_a_reading_room_visit.web.Models;
using book_a_reading_room_visit.web.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.web.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private IAdvancedOrderService _advancedOrderService;
        private readonly IAvailabilityService _availabilityService;
        private readonly IConfiguration _configuration;

        public BookingController(IBookingService bookingService, IAvailabilityService availabilityService,
                                    ChannelFactory<IAdvancedOrderService> channelFactory, IConfiguration configuration)
        {
            _bookingService = bookingService;
            _availabilityService = availabilityService;
            _advancedOrderService = channelFactory.CreateChannel();
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> SecureBooking(BookingViewModel bookingViewModel)
        {
            bookingViewModel.BookingEndDate = bookingViewModel.BookingStartDate;

            var model = new BookingModel
            {
                BookingType = bookingViewModel.BookingType,
                SeatType = bookingViewModel.SeatType,
                VisitStartDate = bookingViewModel.BookingStartDate,
                VisitEndDate = bookingViewModel.BookingEndDate
            };

            var result = await _bookingService.CreateBookingAsync(model);

            if (!result.IsSuccess)
            {
                var routeValues = new
                {
                    bookingtype = bookingViewModel.BookingType.ToStringURL(),
                    seattype = bookingViewModel.SeatType.ToStringURL(),
                    errorcode = ErrorCode.seat_unavailable
                };
                return RedirectToAction("Availability", "Home", routeValues);
            }
            return RedirectToAction("SecureBooking", "Booking", new { bookingReference = result.BookingReference });
        }

        [HttpGet]
        public async Task<IActionResult> SecureBooking(string bookingReference)
        {
            if (string.IsNullOrWhiteSpace(bookingReference))
            {
                return NotFound();
            }
            var model = await _bookingService.GetBookingAsync(bookingReference);

            var bookingViewModel = new BookingViewModel
            {
                BookingReference = bookingReference,
                BookingType = model.BookingType,
                SeatType = model.SeatType,
                BookingStartDate = model.VisitStartDate,
                BookingEndDate = model.VisitEndDate
            };

            var elapsedTime = _configuration.GetValue<int>("Booking:ProvisionalElapsedTime");
            var gmtTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Environment.GetEnvironmentVariable("TimeZone"));
            bookingViewModel.ExpiredBy = TimeZoneInfo.ConvertTimeFromUtc(model.CreatedDate.AddMinutes(elapsedTime), gmtTimeZone);
            var currentTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, gmtTimeZone);
            bookingViewModel.TimeRemaining = Math.Round(bookingViewModel.ExpiredBy.Subtract(currentTime).TotalSeconds);
            if (bookingViewModel.TimeRemaining <= 0)
            {
                var routeValues = new
                {
                    bookingtype = bookingViewModel.BookingType.ToStringURL(),
                    seattype = bookingViewModel.SeatType.ToStringURL(),
                    errorcode = ErrorCode.reserved_time_expired
                };
                return RedirectToAction("Availability", "Home", routeValues);
            }

            return View(bookingViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CancelProvision(BookingViewModel bookingViewModel)
        {
            await _bookingService.DeleteBookingAsync(bookingViewModel.BookingReference);
            var routeValues = new
            {
                bookingtype = bookingViewModel.BookingType.ToStringURL(),
                seattype = bookingViewModel.SeatType.ToStringURL()
            };
            return RedirectToAction("Availability", "Home", routeValues);
        }

        [HttpPost]
        public async Task<IActionResult> BookingConfirmation(BookingViewModel bookingViewModel)
        {
            var gmtTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Environment.GetEnvironmentVariable("TimeZone"));
            var currentTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, gmtTimeZone);
            bookingViewModel.TimeRemaining = Math.Round(bookingViewModel.ExpiredBy.Subtract(currentTime).TotalSeconds);
            if (bookingViewModel.TimeRemaining <= 0)
            {
                var routeValues = new
                {
                    bookingtype = bookingViewModel.BookingType.ToStringURL(),
                    seattype = bookingViewModel.SeatType.ToStringURL(),
                    errorcode = ErrorCode.reserved_time_expired
                };
                return RedirectToAction("Availability", "Home", routeValues);
            }

            if (bookingViewModel.ReaderTicket == 0 ||
                !_advancedOrderService.IsReaderTicketValid(bookingViewModel.ReaderTicket.ToString()))
            {
                ModelState.AddModelError("Ticket", Constants.Valid_Ticket_Required);
            }
            if (bool.TryParse(Environment.GetEnvironmentVariable("EnforceOrderLimit"), out var enforceOrderLimit) && enforceOrderLimit)
            {
                var validation = await _bookingService.GetReaderTicketEligibleAsync(bookingViewModel.ReaderTicket, bookingViewModel.BookingStartDate);
                if (validation == ValidationResult.HaveAnotherVisitOnThisDate)
                {
                    ModelState.AddModelError("Ticket", Constants.Another_Booking_On_The_Same_date);
                }
                else if (validation == ValidationResult.ExceededTheSetLimit)
                {
                    ModelState.AddModelError("Ticket", Constants.Ticket_Exceed_Order_Limit);
                }
            }
            if (string.IsNullOrWhiteSpace(bookingViewModel.Phone) && string.IsNullOrWhiteSpace(bookingViewModel.Email))
            {
                ModelState.AddModelError("email-phone", Constants.Phone_Or_Email_Required);
            }

            if (!ModelState.IsValid)
            {
                return View("SecureBooking", bookingViewModel);
            }

            var model = new BookingModel
            {
                BookingReference = bookingViewModel.BookingReference,
                ReaderTicket = bookingViewModel.ReaderTicket,
                FirstName = bookingViewModel.FirstName,
                LastName = bookingViewModel.LastName,
                Email = bookingViewModel.Email,
                Phone = bookingViewModel.Phone,
                IsAcceptTsAndCs = bookingViewModel.AcceptTsAndCs
            };

            var result = await _bookingService.ReserveSpaceAsync(model);

            if (!result.IsSuccess)
            {
                var routeValues = new
                {
                    bookingtype = bookingViewModel.BookingType.ToStringURL(),
                    seattype = bookingViewModel.SeatType.ToStringURL(),
                    errorcode = ErrorCode.reserved_time_expired
                };
                return RedirectToAction("Availability", "Home", routeValues);
            }
            bookingViewModel.CompleteByDate = result.CompleteByDate;
            bookingViewModel.SeatNumber = result.SeatNumber;

            return View(bookingViewModel);
        }

        [HttpPost]
        public IActionResult CancelBooking(CancelViewModel cancelViewModel)
        {
            return View(cancelViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CancellationConfirmation(CancelViewModel cancelViewModel)
        {
            var model = new BookingCancellationModel
            {
                ReaderTicket = cancelViewModel.ReaderTicket,
                BookingReference = cancelViewModel.BookingReference,
                CancelledBy = "visitor"
            };
            await _bookingService.CancelBookingAsync(model);
            return View(cancelViewModel);
        }

        [HttpGet]
        public IActionResult ReturnToBooking()
        {
            var model = new ReturnToBookingViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ReturnToBooking(ReturnToBookingViewModel returnToBookingViewModel)
        {
            if (returnToBookingViewModel.ReaderTicket == 0)
            {
                ModelState.AddModelError("ticket", Constants.Valid_Ticket_Required);
            }
            if (!ModelState.IsValid)
            {
                return View(returnToBookingViewModel);
            }
            var model = await _bookingService.GetBookingAsync(returnToBookingViewModel.ReaderTicket, returnToBookingViewModel.BookingReference.Trim());
            if (model == null)
            {
                ModelState.AddModelError("ticket-booking-reference", Constants.Valid_Ticket_And_BookingReference_Required);
                return View(returnToBookingViewModel);
            }
            else if (model.BookingStatus == BookingStatuses.Cancelled)
            {
                ModelState.AddModelError("BookingReference", Constants.BookingReference_Is_Cancelled);
                return View(returnToBookingViewModel);
            }
            if (model.BookingStatus == BookingStatuses.Submitted)
            {
                var routeValues = new
                {
                    bookingType = model.BookingType.ToStringURL(),
                    bookingReference = model.BookingReference,
                    readerTicket = model.ReaderTicket
                };
                return RedirectToAction("OrderComplete", "DocumentOrder", routeValues);
            }
            else
            {
                var routeValues = new
                {
                    bookingType = model.BookingType.ToStringURL(),
                    bookingReference = model.BookingReference,
                    readerTicket = model.ReaderTicket
                };
                return RedirectToAction("OrderDocuments", "DocumentOrder", routeValues);
            }
        }

        public IActionResult ThankYou(BookingTypes bookingType)
        {
            return View();
        }
    }
}

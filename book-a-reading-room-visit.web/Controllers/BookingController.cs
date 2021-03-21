using book_a_reading_room_visit.model;
using book_a_reading_room_visit.web.Helper;
using book_a_reading_room_visit.web.Models;
using book_a_reading_room_visit.web.Service;
using Microsoft.AspNetCore.Mvc;
using System.ServiceModel;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.web.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private IAdvancedOrderService _advancedOrderService;
        private readonly IAvailabilityService _availabilityService;

        public BookingController(IBookingService bookingService, IAvailabilityService availabilityService, ChannelFactory<IAdvancedOrderService> channelFactory)
        {
            _bookingService = bookingService;
            _availabilityService = availabilityService;
            _advancedOrderService = channelFactory.CreateChannel();
        }

        [HttpPost]
        public async Task<IActionResult> SecureBooking(BookingViewModel bookingViewModel)
        {
            bookingViewModel.BookingEndDate = bookingViewModel.BookingType == BookingTypes.StandardOrderVisit ? 
                                              bookingViewModel.BookingStartDate : bookingViewModel.BookingStartDate.AddDays(1);

            var result = await _bookingService.CreateBookingAsync(bookingViewModel);

            if (!result.IsSuccess)
            {
                var routeValues = new { 
                    bookingtype = bookingViewModel.BookingType.ToStringURL(), 
                    seattype = bookingViewModel.SeatType.ToStringURL(),
                    errorcode = ErrorCode.seat_unavailable
                };
                return RedirectToAction("Availability", "Home", routeValues);
            }
            bookingViewModel.BookingReference = result.BookingReference;

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
            if (bookingViewModel.ReadingTicket == 0 ||
                !_advancedOrderService.IsReaderTicketValid(bookingViewModel.ReadingTicket.ToString()))
            {
                ModelState.AddModelError("Ticket", Constants.Valid_Ticket_Required);
            }
            if (string.IsNullOrWhiteSpace(bookingViewModel.FirstName))
            {
                ModelState.AddModelError("Firstname", Constants.Firstname_Required);
            }
            if (string.IsNullOrWhiteSpace(bookingViewModel.LastName))
            {
                ModelState.AddModelError("Lastname", Constants.Lastname_Required);
            }
            if (string.IsNullOrWhiteSpace(bookingViewModel.Phone) && string.IsNullOrWhiteSpace(bookingViewModel.Email))
            {
                ModelState.AddModelError("Phone", Constants.Phone_Or_Email_Required);
            }

            if (!ModelState.IsValid)
            {
                return View("SecureBooking", bookingViewModel);
            }

            var result = await _bookingService.ReserveSpaceAsync(bookingViewModel);

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
            await _bookingService.CancelBookingAsync(cancelViewModel);
            return View();
        }
    }
}

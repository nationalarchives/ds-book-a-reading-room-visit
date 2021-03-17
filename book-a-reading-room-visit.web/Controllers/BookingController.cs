using book_a_reading_room_visit.domain;
using book_a_reading_room_visit.web.Helper;
using book_a_reading_room_visit.web.Models;
using book_a_reading_room_visit.web.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    seattype = bookingViewModel.SeatType, 
                    errormessage = "There is no seat available for the given date, please choose a different date" 
                };
                return RedirectToAction("Availability", "Home", routeValues);
            }
            bookingViewModel.BookingReference = result.BookingReference;

            return View(bookingViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> BookingConfirmation(BookingViewModel bookingViewModel, string submitbutton)
        {
            if (submitbutton == "cancel")
            {
                var routeValues = new
                {
                    bookingtype = bookingViewModel.BookingType.ToStringURL(),
                    seattype = bookingViewModel.SeatType
                };
                return RedirectToAction("Availability", "Home", routeValues);
            }

            if (bookingViewModel.ReadingTicket == 0)
            {
                ModelState.AddModelError("Ticket", "Valid reader ticket required.");
            }

            var visitorDetails = _advancedOrderService.GetVisitorDetailsByTicketNo(bookingViewModel.ReadingTicket.ToString());
            if (visitorDetails?.ReaderTicket == null)
            {
                ModelState.AddModelError("Ticket", "Valid reader ticket required.");
            }

            if (!ModelState.IsValid)
            {
                return View("SecureBooking", bookingViewModel);
            }

            bookingViewModel.FirstName = visitorDetails.Firstname;
            bookingViewModel.LastName = visitorDetails.Lastname;
            bookingViewModel.Phone = visitorDetails.Phone;

            var result = await _bookingService.ReserveSpaceAsync(bookingViewModel);

            if (!result.IsSuccess)
            {
                var routeValues = new
                {
                    bookingtype = bookingViewModel.BookingType.ToStringURL(),
                    seattype = bookingViewModel.SeatType,
                    errormessage = "The selected seat is no longer available, please select again."
                };
                return RedirectToAction("Availability", "Home", routeValues);
            }

            return View(bookingViewModel);
        }

        public IActionResult CancelBooking()
        {
            return View();
        }

        public IActionResult CancellationConfirmation()
        {
            return View();
        }
    }
}

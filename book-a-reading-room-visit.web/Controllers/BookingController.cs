using book_a_reading_room_visit.domain;
using book_a_reading_room_visit.web.Helper;
using book_a_reading_room_visit.web.Models;
using book_a_reading_room_visit.web.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.web.Controllers
{
    public class BookingController : Controller
    {
        private readonly AvailabilityService _availabilityService;

        public BookingController(AvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        [HttpPost]
        public async Task<IActionResult> SecureBooking(string bookingType, SeatTypes seatType, string bookingDate)
        {
            var model = new BookingViewModel
            {
                BookingType = bookingType.ToBookingType(),
                SeatType = seatType,
                BookingStartDate = DateTime.Parse(bookingDate),
                BookingEndDate = bookingType.ToBookingType() == BookingTypes.StandardOrderVisit ? DateTime.Parse(bookingDate) : DateTime.Parse(bookingDate).AddDays(1)
            };

            return View(model);
        }

        public IActionResult BookingConfirmation(string bookingType, string bookingReference)
        {
            var model = new BookingViewModel
            {
                BookingType = bookingType.ToBookingType(),
                BookingReference = bookingReference
            };

            return View(model);
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

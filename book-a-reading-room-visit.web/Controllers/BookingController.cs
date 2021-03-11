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
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
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

            var result = await _bookingService.CreateBookingAsync(model);

            if (!result.IsSuccess)
            {

            }
            model.BookingReference = result.BookingReference;

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
    }
}

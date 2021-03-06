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
        public async Task<IActionResult> SecureBooking(string orderType, RoomType roomType, string bookingDate)
        {
            var model = new BookingViewModel
            {
                OrderType = orderType.ToOrderType(),
                RoomType = roomType,
                BookingDate = DateTime.Parse(bookingDate)
            };

            return View(model);
        }

        public IActionResult BookingConfirmation(string orderType, string bookingReference)
        {
            var model = new BookingViewModel
            {
                OrderType = orderType.ToOrderType(),
                BookingReference = bookingReference
            };

            return View(model);
        }
    }
}

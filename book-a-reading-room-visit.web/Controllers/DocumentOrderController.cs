using book_a_reading_room_visit.web.Models;
using book_a_reading_room_visit.web.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.web.Controllers
{
    public class DocumentOrderController : Controller
    {
        public IActionResult OrderDocuments(string bookingType, string bookingReference)
        {
            var model = new BookingViewModel
            {
                BookingType = bookingType.ToBookingType(),
                BookingReference = bookingReference
            };

            return View(model);
        }

        public IActionResult DocumentOrder(string bookingType, string bookingReference)
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

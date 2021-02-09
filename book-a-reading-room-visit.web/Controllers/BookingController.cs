using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.web.Controllers
{
    public class BookingController : Controller
    {
        [Route("book-a-visit/secure-booking")]
        public IActionResult SecureBooking()
        {
            return View();
        }

        [Route("book-a-visit/booking-confirmation")]
        public IActionResult BookingConfirmation()
        {
            return View();
        }
    }
}

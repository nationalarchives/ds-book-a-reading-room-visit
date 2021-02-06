using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.web.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult SecureBooking()
        {
            return View();
        }

        public IActionResult BookingConfirmation()
        {
            return View();
        }
    }
}

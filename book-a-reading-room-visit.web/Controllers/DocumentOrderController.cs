using book_a_reading_room_visit.web.Models;
using book_a_reading_room_visit.web.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using book_a_reading_room_visit.domain;
using book_a_reading_room_visit.web.Service;

namespace book_a_reading_room_visit.web.Controllers
{
    public class DocumentOrderController : Controller
    {

        private readonly IBookingService _bookingService;

        public DocumentOrderController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        public IActionResult OrderDocuments(BookingTypes bookingType, int readerticket, string bookingReference)
        {
            var model = new BookingViewModel
            {
                BookingType = bookingType,
                Ticket = readerticket.ToString(),
                BookingReference = bookingReference
            };

            return View(model);
        }

        public IActionResult OrderComplete(BookingTypes bookingType, int readerticket, string bookingReference)
        {
            var model = new BookingViewModel
            {
                BookingType = bookingType,
                Ticket = readerticket.ToString(),
                BookingReference = bookingReference
            };

            return View(model);
        }
    }
}

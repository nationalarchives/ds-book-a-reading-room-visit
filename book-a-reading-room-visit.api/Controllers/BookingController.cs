using book_a_reading_room_visit.api.Models;
using book_a_reading_room_visit.api.Service;
using book_a_reading_room_visit.domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace book_a_reading_room_visit.api.Controllers
{
    [Route("book-a-visit-api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("search")]
        public async Task<ActionResult<Booking>> GetBookings([FromQuery]BookingSearchModel bs)
        {
            var result = await _bookingService.GetBookingSummaryAsync(bs);
            return Ok(result);
        }
    }
}

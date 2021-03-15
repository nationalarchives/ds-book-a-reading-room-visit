﻿using book_a_reading_room_visit.api.Models;
using book_a_reading_room_visit.api.Service;
using book_a_reading_room_visit.domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace book_a_reading_room_visit.api.Controllers
{
    [Route("book-a-visit-api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("search")]
        public async Task<ActionResult<Booking>> GetBookings([FromQuery]BookingSearchModel bookingSearchModel)
        {
            var result = await _bookingService.GetBookingSummaryAsync(bookingSearchModel);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<string>> CreateBooking(BookingModel bookingModel)
        {
            var result = await _bookingService.CreateBookingAsync(bookingModel);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<Booking>> Get(string bookingReference)
        {
            var booking = await _bookingService.GetBookingByReference(bookingReference);

            if (booking != null)
            {
                return Ok(booking);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int bookingId)
        {
            bool result = await _bookingService.DeleteBooking(bookingId);

            if(result)
            {
                return Ok($"Booking with ID {bookingId} deleted successfully.");
            }
            else
            {
                return NotFound();
            }
        }            

    }
}

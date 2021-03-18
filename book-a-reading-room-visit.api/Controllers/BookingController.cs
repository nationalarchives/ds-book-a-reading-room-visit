using book_a_reading_room_visit.api.Models;
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
        public async Task<ActionResult<Booking>> SearchBookings([FromQuery]BookingSearchModel bookingSearchModel)
        {
            var result = await _bookingService.BookingSearchAsync(bookingSearchModel);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<BookingResponseModel>> CreateBooking(BookingModel bookingModel)
        {
            var result = await _bookingService.CreateBookingAsync(bookingModel);
            return Ok(result);
        }

        [HttpGet]
        [Route("{bookingId:int}")]
        public async Task<ActionResult<Booking>> Get(int bookingId)
        {
            var booking = await _bookingService.GetBookingByIdAsync(bookingId);

            if (booking != null)
            {
                return Ok(booking);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("update-reader-ticket")]
        public async Task<ActionResult<BookingResponseModel>> UpdateReaderTicket(BookingModel bookingModel)
        {
            var result = await _bookingService.UpdateReaderTicketAsync(bookingModel);
            return Ok(result);
        }

        [HttpPost("update-reserved-seat")]
        public async Task<ActionResult<BookingResponseModel>> UpdateReservedSeat([FromBody] KewBookingSeatUpdateModel model)
        {
            BookingResponseModel result = await _bookingService.UpdateSeatBookingAsync(model.BookingId, model.NewSeatId);

            if(result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return Conflict(result.ErrorMessage);
            }
            
        }
    }
}

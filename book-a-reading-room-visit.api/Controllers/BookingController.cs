using book_a_reading_room_visit.api.Service;
using book_a_reading_room_visit.domain;
using book_a_reading_room_visit.model;
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

        [HttpPost("create")]
        public async Task<ActionResult<BookingResponseModel>> CreateBooking(BookingModel bookingModel)
        {
            var result = await _bookingService.CreateBookingAsync(bookingModel);
            return Ok(result);
        }
        
        [HttpPost("confirm")]
        public async Task<ActionResult<BookingResponseModel>> ConfirmBooking(BookingModel bookingModel)
        {
            var result = await _bookingService.ConfirmBookingAsync(bookingModel);
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

        [HttpPost("cancel")]
        public async Task<ActionResult<BookingResponseModel>> CancelBooking(BookingCancellationModel bookingCancellationModel)
        {
            var result = await _bookingService.CancelBookingAsync(bookingCancellationModel);

            if (result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                if(result.ErrorMessage.Contains("no booking found"))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500);
                }
            }
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

        [HttpGet]
        [Route("{readerticket}/{bookingreference}")]
        public async Task<ActionResult<Booking>> GetByReference(int readerTicket, string bookingReference)
        {
            var booking = await _bookingService.GetBookingByReferenceAsync(readerTicket, bookingReference);

            if (booking != null)
            {
                return Ok(booking);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<Booking>> SearchBookings([FromQuery]BookingSearchModel bookingSearchModel)
        {
            var result = await _bookingService.BookingSearchAsync(bookingSearchModel);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{bookingreference}")]
        public async Task<IActionResult> DeleteBooking(string bookingreference)
        {
            var result = await _bookingService.DeleteBookingAsync(bookingreference);
            return result ? NoContent() : NotFound();
        }
    }
}

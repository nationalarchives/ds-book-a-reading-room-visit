﻿using book_a_reading_room_visit.api.Service;
using book_a_reading_room_visit.domain;
using book_a_reading_room_visit.model;
using Microsoft.AspNetCore.Mvc;

namespace book_a_reading_room_visit.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ILogger _logger;

        public BookingController(IBookingService bookingService, ILogger<BookingController> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<ActionResult<BookingResponseModel>> CreateBooking(BookingModel bookingModel)
        {
            var result = await _bookingService.CreateBookingAsync(bookingModel);
            return Ok(result);
        }

        [HttpPost("create-multi-day")]
        public async Task<ActionResult<BookingResponseModel>> CreateMultiDayBooking(BookingModelMultiDay bookingModelMultiDay)
        {
            var result = await _bookingService.CreateMultiDayBookingAsync(bookingModelMultiDay);
            return Ok(result);
        }

        [HttpPost("confirm")]
        public async Task<ActionResult<BookingResponseModel>> ConfirmBooking(BookingModel bookingModel)
        {
            var result = await _bookingService.ConfirmBookingAsync(bookingModel);
            return Ok(result);
        }

        [HttpPost("upsert-document")]
        public async Task<ActionResult<BookingResponseModel>> UpsertDocument(BookingModel bookingModel)
        {
            var result = await _bookingService.UpsertDocumentsAsync(bookingModel);
            return Ok(result);
        }

        [HttpPost("update-reserved-seat")]
        public async Task<ActionResult<BookingResponseModel>> UpdateReservedSeat([FromBody] KewBookingSeatUpdateModel model)
        {
            BookingResponseModel result = await _bookingService.UpdateSeatBookingAsync(model.BookingId, model.NewSeatId, model.Comment, model.UpdatedBy);

            if (result.IsSuccess)
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
                if (result.ErrorMessage.Contains("no booking found"))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500);
                }
            }
        }

        [HttpPost("update-comments")]
        public async Task<ActionResult<bool>> UpdateComments([FromBody] BookingCommentsModel bookingCommentsModel)
        {
            bool result = await _bookingService.UpdateBookingCommentsAsync(bookingCommentsModel);
            if (result)
            {
               return Ok() ;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("toggle-no-show")]
        public async Task<ActionResult<bool>> ToggleNoShow ([FromBody]int bookingId)
        {
            bool result = await _bookingService.ToggleNoShowAsync(bookingId);
            if (result)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{bookingId:int}")]
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

        [HttpGet("{bookingreference}")]
        public async Task<ActionResult<Booking>> Get(string bookingreference)
        {
            var booking = await _bookingService.GetBookingByReferenceAsync(bookingreference);

            if (booking != null)
            {
                return Ok(booking);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("readerticket-eligible")]
        public async Task<ActionResult<ValidationResult>> GetReaderTicketEligible(int readerTicket, DateTime visitDate)
        {
            var result = await _bookingService.GetReaderTicketEligibilityAsync(readerTicket, visitDate);
            return Ok(result);
        }

        [HttpGet("{readerticket}/{bookingreference}")]
        public async Task<ActionResult<Booking>> GetByReference(int readerTicket, string bookingReference)
        {
            var booking = await _bookingService.GetBookingByReaderTicketAndReferenceAsync(readerTicket, bookingReference);

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
        public async Task<ActionResult<Booking>> SearchBookings([FromQuery] BookingSearchModel bookingSearchModel)
        {
            var result = await _bookingService.BookingSearchAsync(bookingSearchModel);
            return Ok(result);
        }

        [HttpDelete("{bookingreference}")]
        public async Task<IActionResult> DeleteBooking(string bookingreference)
        {
            var result = await _bookingService.DeleteBookingAsync(bookingreference);
            return result ? NoContent() : NotFound();
        }

        [HttpPost("submit")]
        public async Task<ActionResult<int>> SubmitBooking(DateTime completeBy)
        {
            int result = await _bookingService.SubmitBookingAsync(completeBy);
            return Ok(result);
        }

        [HttpPost("send-confirmation")]
        public async Task<ActionResult<int>> SendBookingConfirmationEmails(DateTime completeBy)
        {
            _logger.LogInformation($"Starting sending confirmation emails for bookings with complete by date {completeBy}.");
            int result = await _bookingService.SendBookingConfirmationEmailsAsync(completeBy);
            _logger.LogInformation($"Finished sending confirmation emails for bookings with complete by date {completeBy}. {result} bookings were processed.");
            return Ok(result);
        }

        [HttpPost("send-notification")]
        public async Task<ActionResult<int>> SendReminderNotificationEmails(DateTime completeBy)
        {
            int result = await _bookingService.SendReminderNotificationEmailsAsync(completeBy);
            return Ok(result);
        }

        [HttpPost("send-post-visit-survey")]
        public async Task<ActionResult<int>> SendPostVisitSurveyEmails(DateTime endDate)
        {
            int result = await _bookingService.SendPostVisitSurveyEmailsAsync(endDate);
            return Ok(result);
        }
    }
}

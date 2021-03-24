using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using book_a_reading_room_visit.web.Service;
using book_a_reading_room_visit.model;
using book_a_reading_room_visit.web.Models;

namespace book_a_reading_room_visit.web.Controllers
{
    public class DocumentOrderController : Controller
    {

        private readonly IBookingService _bookingService;

        public DocumentOrderController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        public async Task<IActionResult> OrderDocuments(BookingTypes bookingType, int readerticket, string bookingReference)
        {
            var bookingModel = await _bookingService.GetBookingAsync(readerticket, bookingReference);

            var model = new BookingViewModel
            {
                BookingType = bookingModel.BookingType,
                Ticket = bookingModel.ReaderTicket?.ToString(),
                BookingReference = bookingModel.BookingReference,
                BookingStartDate = bookingModel.VisitStartDate,
                CompleteByDate = bookingModel.CompleteByDate,
                SeatType = bookingModel.SeatType,
                SeatNumber = bookingModel.SeatNumber
            };

            return View(model);
        }

        public async Task<IActionResult> OrderComplete(BookingTypes bookingType, int readerticket, string bookingReference)
        {
            var bookingModel = await _bookingService.GetBookingAsync(readerticket, bookingReference);

            var model = new BookingViewModel
            {
                BookingType = bookingModel.BookingType,
                Ticket = bookingModel.ReaderTicket?.ToString(),
                BookingReference = bookingModel.BookingReference,
                BookingStartDate = bookingModel.VisitStartDate,
                CompleteByDate = bookingModel.CompleteByDate,
                SeatType = bookingModel.SeatType,
                SeatNumber = bookingModel.SeatNumber
            };
            return View(model);
        }
    }
}

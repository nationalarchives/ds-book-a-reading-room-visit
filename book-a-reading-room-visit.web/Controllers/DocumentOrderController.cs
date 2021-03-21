using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using book_a_reading_room_visit.web.Service;
using book_a_reading_room_visit.model;

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
            var model = await _bookingService.GetBookingAsync(readerticket, bookingReference);
            return View(model);
        }

        public async Task<IActionResult> OrderComplete(BookingTypes bookingType, int readerticket, string bookingReference)
        {
            var model = await _bookingService.GetBookingAsync(readerticket, bookingReference);
            return View(model);
        }
    }
}

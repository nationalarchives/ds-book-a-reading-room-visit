using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using book_a_reading_room_visit.web.Service;
using book_a_reading_room_visit.model;
using book_a_reading_room_visit.web.Models;
using book_a_reading_room_visit.web.Helper;

namespace book_a_reading_room_visit.web.Controllers
{
    public class DocumentOrderController : Controller
    {

        private readonly IBookingService _bookingService;
        private readonly ValidateDocumentOrder _validateDocumentOrder;

        public DocumentOrderController(IBookingService bookingService, ValidateDocumentOrder validateDocumentOrder)
        {
            _bookingService = bookingService;
            _validateDocumentOrder = validateDocumentOrder;
        }

        public async Task<IActionResult> ContinueLater(BookingTypes bookingType, string bookingReference)
        {
            var bookingModel = await _bookingService.GetBookingAsync(bookingReference);

            var model = bookingModel.MapToDocumentOrderViewModel();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> OrderDocuments(BookingTypes bookingType, int readerticket, string bookingReference)
        {
            var bookingModel = await _bookingService.GetBookingAsync(readerticket, bookingReference);
            if (bookingModel.BookingStatus == BookingStatuses.Created)
            {
                var model = bookingModel.MapToDocumentOrderViewModel();
                return View(model);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> OrderDocuments(DocumentOrderViewModel model)
        {
            if (_validateDocumentOrder.IsValid(ModelState, model, out var validatedDocs))
            {
                var bookingModel = model.MapToBookingModel(validatedDocs);
                var response = await _bookingService.UpsertDocumentAsync(bookingModel);

                if (response.IsSuccess)
                {
                    var routeValues = new
                    {
                        bookingtype = model.BookingType.ToStringURL(),
                        readerticket = model.ReaderTicket,
                        bookingReference = model.BookingReference
                    };
                    return RedirectToAction("OrderComplete", "DocumentOrder", routeValues);
                }
                ModelState.AddModelError("", response.ErrorMessage);
            }
            return View(model);
        }

        public async Task<IActionResult> OrderComplete(BookingTypes bookingType, int readerticket, string bookingReference)
        {
            var bookingModel = await _bookingService.GetBookingAsync(readerticket, bookingReference);
            if (bookingModel.BookingStatus == BookingStatuses.Cancelled)
            {
                return RedirectToAction("Error", "Home");
            }
            var model = bookingModel.MapToOrderCompleteViewModel();
            return View(model);
        }
    }
}

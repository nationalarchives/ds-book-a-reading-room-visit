using book_a_reading_room_visit.model;
using book_a_reading_room_visit.web.Helper;
using book_a_reading_room_visit.web.Models;
using book_a_reading_room_visit.web.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAvailabilityService _availabilityService;

        public HomeController(ILogger<HomeController> logger, IAvailabilityService availabilityService)
        {
            _logger = logger;
            _availabilityService = availabilityService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _availabilityService.GetAvailabilitySummaryAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Availability(BookingTypes bookingType, SeatTypes seatType, ErrorCode errorCode)
        {
            if (seatType == default(SeatTypes))
            {
                seatType = bookingType == BookingTypes.StandardOrderVisit ? SeatTypes.StdRRSeat : SeatTypes.BulkOrderSeat;
            }
            var model = new AvailabilityViewModel
            {
                BookingType = bookingType,
                SeatType = seatType,
                AvailableBookings = await _availabilityService.GetAvailabilityAsync(seatType),
                ErrorMessage = errorCode.ToMessage()
            };
            return View(model);
        }

        public IActionResult ReturnToBooking()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using book_a_reading_room_visit.domain;
using book_a_reading_room_visit.web.Models;
using book_a_reading_room_visit.web.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public async Task<IActionResult> Availability(string orderType)
        {
            var seatType = orderType.ToOrderType() == OrderType.StandardOrderVisit ? SeatTypes.StdRRSeat : SeatTypes.BulkOrderSeat;
            var roomType = orderType.ToOrderType() == OrderType.StandardOrderVisit ? RoomType.StandardReadingRoom : RoomType.None;

            var model = new AvailabilityViewModel
            {
                OrderType = orderType.ToOrderType(),
                RoomType = roomType,
                AvailableBookings = await _availabilityService.GetAvailabilityAsync(seatType)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Availability(string orderType, RoomType roomType)
        {
            var seatType = SeatTypes.BulkOrderSeat;
            if (orderType.ToOrderType() == OrderType.StandardOrderVisit)
            {
                seatType = roomType == RoomType.StandardReadingRoom ? SeatTypes.StdRRSeat : SeatTypes.MandLRR;
            }
            var model = new AvailabilityViewModel
            {
                RoomType = roomType,
                OrderType = orderType.ToOrderType(),
                AvailableBookings = await _availabilityService.GetAvailabilityAsync(seatType)
            };

            return View(model);
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

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
        private readonly AvailabilityService _availabilityService;

        public HomeController(ILogger<HomeController> logger, AvailabilityService availabilityService)
        {
            _logger = logger;
            _availabilityService = availabilityService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _availabilityService.GetAvailabilitySummaryAsync();
            return View(model);
        }

        public IActionResult Availability(string orderType)
        {
            var model = orderType.ToOrderType();
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

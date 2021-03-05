using book_a_reading_room_visit.api.Models;
using book_a_reading_room_visit.api.Service;
using book_a_reading_room_visit.domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Controllers
{
    [Route("book-a-visit-api/[controller]")]
    [ApiController]
    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityService _availabilityService;

        public AvailabilityController(IAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        [HttpGet("summary")]
        public async Task<ActionResult<AvailabilitySummaryModel>> GetAvailabilitySummary()
        {
            var result = await _availabilityService.GetAvailabilitySummaryAsync();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<AvailabilitySummaryModel>> GetAvailability(SeatTypes seatType)
        {
            var result = await _availabilityService.GetAvailabilityAsync(seatType);
            return Ok(result);
        }
    }
}

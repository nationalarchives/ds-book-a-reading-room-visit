using book_a_reading_room_visit.api.Models;
using book_a_reading_room_visit.api.Service;
using book_a_reading_room_visit.domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Controllers
{
    [Route("book-a-visit-api/[controller]")]
    [ApiController]
    public class AvailabilityController : ControllerBase
    {
        private readonly AvailabilityService _availabilityService;

        public AvailabilityController(AvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        [HttpGet("summary")]
        public async Task<ActionResult<AvailabilitySummaryModel>> GetAvailabilitySummary()
        {
            var result = await _availabilityService.GetAvailabilitySummaryAsync();
            return Ok(result);
        }
    }
}

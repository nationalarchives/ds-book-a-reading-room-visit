﻿using book_a_reading_room_visit.api.Service;
using book_a_reading_room_visit.domain;
using book_a_reading_room_visit.model;
using Microsoft.AspNetCore.Mvc;

namespace book_a_reading_room_visit.api.Controllers
{
    [Route("[controller]")]
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

        [HttpGet("seats-count-by-seattype")]
        public async Task<ActionResult<List<AvailabilityModel>>> GetAvailability(SeatTypes seatType)
        {
            var result = await _availabilityService.GetAvailabilityAsync(seatType);
            return Ok(result);
        }

        [HttpGet("seats-by-seattype")]
        public async Task<ActionResult<List<Seat>>> GetAvailabileSeats(SeatTypes seatType, DateTime availableOn)
        {
            var result = await _availabilityService.GetAvailabileSeatsAsync(seatType, availableOn);
            return Ok(result);
        }

        [HttpGet("all-seats")]
        public async Task<ActionResult<List<Seat>>> GetAllAvailabileSeats(DateTime availableOn, BookingTypes bookingType, bool includeManagerialDiscretion = false)
        {
            var result = await _availabilityService.GetAllAvailabileSeatsAsync(availableOn, bookingType, includeManagerialDiscretion);
            return Ok(result);
        }

        [HttpGet("seats-multi-day-visit")]
        public async Task<ActionResult<List<Seat>>> GetAllAvailabileSeatsForMultiDayVisit(DateTime visitStartDate, DateTime visitEndDate, bool includeManagerialDiscretion = false)
        {
            var result = await _availabilityService.GetAvailabileSeatsForMultiDayVisitAsync(visitStartDate, visitEndDate, includeManagerialDiscretion);
            return Ok(result);
        }
    }
}

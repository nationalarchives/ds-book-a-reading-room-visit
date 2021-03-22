using book_a_reading_room_visit.domain;
using book_a_reading_room_visit.model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Service
{
    public interface IAvailabilityService
    {
        Task<AvailabilitySummaryModel> GetAvailabilitySummaryAsync();
        Task<List<AvailabilityModel>> GetAvailabilityAsync(SeatTypes seatType);
        Task<List<Seat>> GetAvailabileSeatsAsync(SeatTypes seatType, DateTime availableOn);
        Task<List<Seat>> GetAllAvailabileSeatsAsync(DateTime availableOn, BookingTypes bookingType);
    }
}

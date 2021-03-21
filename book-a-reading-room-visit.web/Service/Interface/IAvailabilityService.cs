using book_a_reading_room_visit.model;
using book_a_reading_room_visit.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.web.Service
{
    public interface IAvailabilityService
    {
        Task<AvailabilitySummaryModel> GetAvailabilitySummaryAsync();
        Task<List<AvailableSeat>> GetAvailabilityAsync(SeatTypes seatType);
    }
}

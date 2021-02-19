using book_a_reading_room_visit.data;
using book_a_reading_room_visit.domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Service
{
    public class AvailabilityService
    {
        private readonly BookingContext _context;

        public AvailabilityService(BookingContext context)
        {
            _context = context;
        }

        public async Task<List<Seat>> GetAllSeatsAsync()
        {
            return await _context.Seats.ToListAsync();
        }
    }
}

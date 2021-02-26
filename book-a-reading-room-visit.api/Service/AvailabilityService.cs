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
            var bookings = from booking in _context.Set<Booking>()
                           join seat in _context.Set<Seat>().Where(s => s.SeatTypeId == 1)
                           on booking.SeatId equals seat.Id
                           select new { booking.VisitStartDate, };

            await GetStandardAvailability();
            return await _context.Seats.ToListAsync();
        }

        public async Task GetStandardAvailability()
        {
            var availableDates = new List<DateTime> { DateTime.Parse("10/03/2021"), DateTime.Parse("11/03/2021"), DateTime.Parse("13/03/2021") };
            var stdOrderSeats = new SeatTypes[] { SeatTypes.StdRRSeat, SeatTypes.StdRRSeatWithCamera, SeatTypes.MandLRR, SeatTypes.MandLRRWithCamera };
            var seatCount = await _context.Seats.CountAsync(s => stdOrderSeats.Contains((SeatTypes)s.SeatTypeId));

            var bookings = await (from booking in _context.Set<Booking>().Where(b => availableDates.Contains(b.VisitStartDate))
                           join seat in _context.Set<Seat>().Where(s => stdOrderSeats.Contains((SeatTypes)s.SeatTypeId))
                           on booking.SeatId equals seat.Id
                           group booking by booking.VisitStartDate
                           into g
                           select new { g.Key, Count = g.Count() }).ToListAsync();

            var bookings2 = await (from booking in _context.Set<Booking>().Where(b => availableDates.Contains(b.VisitStartDate))
                           join seat in _context.Set<Seat>().Where(s => stdOrderSeats.Contains((SeatTypes)s.SeatTypeId))
                           on booking.SeatId equals seat.Id
                           select new { seat.Number }).CountAsync();

            var bookings3 = seatCount - bookings2;
        }
    }
}

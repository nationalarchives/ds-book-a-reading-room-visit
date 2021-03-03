using book_a_reading_room_visit.api.Models;
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
        private readonly WorkingDayService _workingDayService;
        private readonly SeatTypes[] StamdardOrderSeats = new SeatTypes[] { SeatTypes.StdRRSeat, SeatTypes.StdRRSeatWithCamera, SeatTypes.MandLRR, SeatTypes.MandLRRWithCamera };
        private readonly SeatTypes[] BulkOrderSeats = new SeatTypes[] { SeatTypes.BulkOrderSeat, SeatTypes.BulkOrderSeatWithCamera };

        public AvailabilityService(BookingContext context, WorkingDayService workingDayService)
        {
            _context = context;
            _workingDayService = workingDayService;
        }

        public async Task<AvailabilitySummaryModel> GetAvailabilitySummaryAsync()
        {
            var availableSummaryModel = new AvailabilitySummaryModel();
            var standardAvailableDates = await _workingDayService.GetStandardOrderAvailableDatesAsync();
            var bulkAvailableDates = await _workingDayService.GetBulkOrderAvailableDatesAsync();

            var stdSeatCount = await _context.Seats.CountAsync(s => StamdardOrderSeats.Contains((SeatTypes)s.SeatTypeId));

            var stdBookingCount = await (from booking in _context.Set<Booking>().Where(b => standardAvailableDates.Contains(b.VisitStartDate))
                                         join seat in _context.Set<Seat>().Where(s => StamdardOrderSeats.Contains((SeatTypes)s.SeatTypeId))
                                         on booking.SeatId equals seat.Id
                                         select new { seat.Number }).CountAsync();

            var bulkSeatCount = await _context.Seats.CountAsync(s => BulkOrderSeats.Contains((SeatTypes)s.SeatTypeId));

            var bulkBookingCount = await (from booking in _context.Set<Booking>().Where(b => bulkAvailableDates.Contains(b.VisitStartDate))
                                          join seat in _context.Set<Seat>().Where(s => BulkOrderSeats.Contains((SeatTypes)s.SeatTypeId))
                                          on booking.SeatId equals seat.Id
                                          select new { seat.Number }).CountAsync();


            availableSummaryModel.StandardBookingAvailable = (standardAvailableDates.Count * stdSeatCount) - stdBookingCount;
            availableSummaryModel.BulkBookingsAvailable = (bulkAvailableDates.Count * bulkSeatCount) - bulkBookingCount;
            return availableSummaryModel;
        }

        public async Task<List<AvailabilityModel>> GetAvailabilityAsync(SeatTypes seatType)
        {
            var availableDates = await _workingDayService.GetStandardOrderAvailableDatesAsync();

            var bookings = await (from booking in _context.Set<Booking>().Where(b => availableDates.Contains(b.VisitStartDate))
                                  join seat in _context.Set<Seat>().Where(s => (SeatTypes)s.SeatTypeId == seatType)
                                  on booking.SeatId equals seat.Id
                                  group booking by booking.VisitStartDate
                                  into g
                                  select new AvailabilityModel { Date = g.Key, AvailableSeats = g.Count() }).ToListAsync();

            return bookings;
        }


    }
}

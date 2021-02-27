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

        public AvailabilityService(BookingContext context, WorkingDayService workingDayService)
        {
            _context = context;
            _workingDayService = workingDayService;
        }

        public async Task<AvailabilitySummaryModel> GetAvailabilitySummaryAsync()
        {
            var availableSummaryModel = new AvailabilitySummaryModel();
            var availableDates = await _workingDayService.GetAvailableDatesAsync();

            var stdSeatCount = await _context.Seats.CountAsync(s => StamdardOrderSeats.Contains((SeatTypes)s.SeatTypeId));

            var stdBookingCount = await (from booking in _context.Set<Booking>().Where(b => availableDates.Contains(b.VisitStartDate))
                                        join seat in _context.Set<Seat>().Where(s => StamdardOrderSeats.Contains((SeatTypes)s.SeatTypeId))
                                        on booking.SeatId equals seat.Id
                                        select new { seat.Number }).CountAsync();

            var bulkSeatCount = await _context.Seats.CountAsync(s => (SeatTypes)s.SeatTypeId == SeatTypes.BulkRRSeat);

            var bulkBookingCount = await (from booking in _context.Set<Booking>().Where(b => availableDates.Contains(b.VisitStartDate))
                                         join seat in _context.Set<Seat>().Where(s => (SeatTypes)s.SeatTypeId == SeatTypes.BulkRRSeat)
                                         on booking.SeatId equals seat.Id
                                         select new { seat.Number }).CountAsync();


            availableSummaryModel.StandardBookingAvailable = (availableDates.Count * stdSeatCount) - stdBookingCount;
            availableSummaryModel.BulkBookingsAvailable = ((availableDates.Count / 2) * bulkSeatCount) - bulkBookingCount;
            return availableSummaryModel;
        }

        
    }
}

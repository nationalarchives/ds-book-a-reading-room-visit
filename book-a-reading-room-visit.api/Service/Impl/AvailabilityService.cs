using book_a_reading_room_visit.api.Models;
using book_a_reading_room_visit.data;
using book_a_reading_room_visit.domain;
using book_a_reading_room_visit.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Service
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly BookingContext _context;
        private readonly IWorkingDayService _workingDayService;
        private readonly SeatTypes[] StandardOrderSeats = new SeatTypes[] { SeatTypes.StdRRSeat, SeatTypes.StdRRSeatWithCamera, SeatTypes.MandLRR, SeatTypes.MandLRRWithCamera };
        private readonly SeatTypes[] BulkOrderSeats = new SeatTypes[] { SeatTypes.BulkOrderSeat, SeatTypes.BulkOrderSeatWithCamera };

        public AvailabilityService(BookingContext context, IWorkingDayService workingDayService)
        {
            _context = context;
            _workingDayService = workingDayService;
        }

        public async Task<AvailabilitySummaryModel> GetAvailabilitySummaryAsync()
        {
            var availableSummaryModel = new AvailabilitySummaryModel();
            var standardAvailableDates = await _workingDayService.GetStandardOrderAvailableDatesAsync();
            var bulkAvailableDates = await _workingDayService.GetBulkOrderAvailableDatesAsync();

            var stdSeatCount = await _context.Seats.CountAsync(s => StandardOrderSeats.Contains((SeatTypes)s.SeatTypeId));

            var stdBookingCount = await (from booking in _context.Set<Booking>().Where(b => standardAvailableDates.Contains(b.VisitStartDate))
                                         join seat in _context.Set<Seat>().Where(s => StandardOrderSeats.Contains((SeatTypes)s.SeatTypeId))
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
            if (StandardOrderSeats.Contains(seatType))
            {
                var stdSeatCount = await _context.Seats.CountAsync(s => (SeatTypes)s.SeatTypeId == seatType);
                var standardAvailableDates = await _workingDayService.GetStandardOrderAvailableDatesAsync();

                var standardBookings = await (from booking in _context.Set<Booking>().Where(b => standardAvailableDates.Contains(b.VisitStartDate))
                                        join seat in _context.Set<Seat>().Where(s => (SeatTypes)s.SeatTypeId == seatType)
                                        on booking.SeatId equals seat.Id
                                        group booking by booking.VisitStartDate
                                        into g
                                        select new AvailabilityModel { Date = g.Key, AvailableSeats = stdSeatCount - g.Count() }).ToListAsync();

                return (from date in standardAvailableDates
                        join booking in standardBookings on date equals booking.Date into lj
                        from subbook in lj.DefaultIfEmpty()
                        select new AvailabilityModel { Date = date, AvailableSeats = subbook?.AvailableSeats ?? stdSeatCount }).ToList();
            }

            var bulkSeatCount = await _context.Seats.CountAsync(s => (SeatTypes)s.SeatTypeId == seatType);
            var bulkAvailableDates = await _workingDayService.GetBulkOrderAvailableDatesAsync();

            var bulkbookings = await (from booking in _context.Set<Booking>().Where(b => bulkAvailableDates.Contains(b.VisitStartDate))
                                  join seat in _context.Set<Seat>().Where(s => (SeatTypes)s.SeatTypeId == seatType)
                                  on booking.SeatId equals seat.Id
                                  group booking by booking.VisitStartDate
                                  into g
                                  select new AvailabilityModel { Date = g.Key, AvailableSeats = bulkSeatCount - g.Count() }).ToListAsync();

            return (from date in bulkAvailableDates
                    join booking in bulkbookings on date equals booking.Date into lj
                    from subbook in lj.DefaultIfEmpty()
                    select new AvailabilityModel { Date = date, AvailableSeats = subbook?.AvailableSeats ?? bulkSeatCount }).ToList();

        }

        public async Task<List<Seat>> GetAvailabileSeatsAsync(SeatTypes seatType, DateTime availableOn)
        {
            return await (from seat in _context.Set<Seat>().Where(s => (SeatTypes)s.SeatTypeId == seatType)
                        join booking in _context.Set<Booking>().Where(b => b.VisitStartDate == availableOn)
                        on seat.Id equals booking.SeatId into lj
                        from subseat in lj.DefaultIfEmpty()
                        where subseat == null
                        select new Seat { Id = seat.Id, Number = seat.Number, SeatType = seat.SeatType, SeatTypeId = seat.SeatTypeId }).ToListAsync();
        }

        public async Task<List<Seat>> GetAllAvailabileSeatsAsync(DateTime availableOn, BookingTypes bookingType)
        {
            var seatTypes = bookingType == BookingTypes.StandardOrderVisit ? StandardOrderSeats.Select(s => (int)s).ToList() 
                                                                           : BulkOrderSeats.Select(s => (int)s).ToList();
            
            var availableSeats = await (from seat in _context.Set<Seat>().Where(s => seatTypes.Contains(s.SeatTypeId))
                                         join booking in _context.Set<Booking>().Where(b => b.VisitStartDate == availableOn)
                                         on seat.Id equals booking.SeatId into lj
                                         from subseat in lj.DefaultIfEmpty()
                                         where subseat == null
                                         select new Seat { Id = seat.Id, Number = seat.Number, SeatType = seat.SeatType, SeatTypeId = seat.SeatTypeId }).ToListAsync();

            return availableSeats;
        }
    }
}

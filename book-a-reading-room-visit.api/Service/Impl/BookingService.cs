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
    public class BookingService : IBookingService
    {
        private readonly BookingContext _context;

        public BookingService(BookingContext context)
        {
            _context = context;
        }

        public async Task<IList<Booking>> GetBookingSummaryAsync(BookingSearchModel bs)
        {
            DateTime? dateComponent = null;

            if(bs.Date.HasValue)
            {
                dateComponent = bs.Date.Value.Date;
            }

            var bookings = await _context.Bookings.Where(b =>
                (bs.BookingReference == null || bs.BookingReference  == b.BookingReference) &&
                (bs.ReadersTicket == null || bs.ReadersTicket == b.ReaderTicket) &&
                (dateComponent == null || dateComponent == b.VisitStartDate.Date)
                ).TagWith<Booking>("Search of Bookings").ToListAsync();
            
            return bookings;
        }
    }
}

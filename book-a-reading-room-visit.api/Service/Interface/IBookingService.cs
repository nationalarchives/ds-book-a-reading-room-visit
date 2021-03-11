using book_a_reading_room_visit.api.Models;
using book_a_reading_room_visit.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Service
{
    public interface IBookingService
    {
        Task<IList<Booking>> GetBookingSummaryAsync(BookingSearchModel bs);
    }
}

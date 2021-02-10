using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.web.Models
{
    public class BookingViewModel
    {
        public string BookingReference { get; set; }
        public OrderType OrderType { get; set; }
    }
}

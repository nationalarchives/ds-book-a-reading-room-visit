using System;

namespace book_a_reading_room_visit.web.Models
{
    public class BookingViewModel
    {
        public string BookingReference { get; set; }
        public OrderType OrderType { get; set; }
        public RoomType RoomType { get; set; }
        public DateTime BookingDate { get; set; }
    }
}

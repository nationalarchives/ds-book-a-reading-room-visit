using System;
using book_a_reading_room_visit.model;

namespace book_a_reading_room_visit.web.Models
{
    public class CancelViewModel
    {
        public BookingTypes BookingType { get; set; }
        public string BookingReference { get; set; }
        public DateTime BookingStartDate { get; set; }
        public SeatTypes SeatType { get; set; }
        public string SeatNumber { get; set; }
        public int ReaderTicket { get; set; }
        public string CancelledBy { get; set; }
    }
}

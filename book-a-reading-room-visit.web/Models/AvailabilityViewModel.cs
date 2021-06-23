using book_a_reading_room_visit.model;
using System;
using System.Collections.Generic;

namespace book_a_reading_room_visit.web.Models
{
    public class AvailabilityViewModel
    {
        public BookingTypes BookingType { get; set; }
        public SeatTypes SeatType { get; set; }
        public DateTime CutOffDate { get; set; }
        public string FirstAvailableDate { get; set; }
        public List<AvailableSeat> AvailableBookings { get; set; }
        public List<AvailableSeatGroup> AvailableSeatGroups { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class AvailableSeat
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public int AvailableSeats { get; set; }
    }

    public class AvailableSeatGroup
    {
        public string Month { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
    }
}

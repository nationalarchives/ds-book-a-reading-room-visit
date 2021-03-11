﻿using book_a_reading_room_visit.domain;
using System;

namespace book_a_reading_room_visit.web.Models
{
    public class BookingViewModel
    {
        public string BookingReference { get; set; }
        public BookingTypes BookingType { get; set; }
        public SeatTypes SeatType { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
    }
}

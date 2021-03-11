using book_a_reading_room_visit.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Models
{
    public class BookingModel
    {
        public string BookingReference { get; set; }
        public bool IsStandardVisit  { get; set; }
        public SeatTypes SeatType { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
    }
}

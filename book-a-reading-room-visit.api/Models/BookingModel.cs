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
        public BookingTypes BookingType  { get; set; }
        public SeatTypes SeatType { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
        public bool AcceptTsAndCs { get; set; }
        public bool AcceptCovidCharter { get; set; }
        public int? ReadingTicket { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

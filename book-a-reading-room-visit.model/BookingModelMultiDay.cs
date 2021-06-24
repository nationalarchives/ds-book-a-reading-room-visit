using System;
using System.Collections.Generic;
using System.Text;

namespace book_a_reading_room_visit.model
{
    public class BookingModelMultiDay
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string ReaderTicket { get; set; }

        public DateTime VisitStartDate { get; set; }

        public DateTime VisitEndDate { get; set; }

        public int SeatId { get; set; }
    }
}

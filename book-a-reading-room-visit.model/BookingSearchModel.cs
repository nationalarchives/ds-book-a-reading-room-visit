using System;

namespace book_a_reading_room_visit.model
{
    public class BookingSearchModel
    {
        public DateTime? Date { get; set; }

        public string BookingReference { get; set; }

        public int? ReadersTicket { get; set; }
    }
}
﻿namespace book_a_reading_room_visit.model
{
    public class BookingCancellationModel
    {
        public int BookingId { get; set; }
        public string BookingReference { get; set; }
        public int ReaderTicket { get; set; }
        public string CancelledBy { get; set; }
    }
}

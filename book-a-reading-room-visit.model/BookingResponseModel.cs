using System;

namespace book_a_reading_room_visit.model
{
    public class BookingResponseModel
    {
        public string BookingReference { get; set; }
        public DateTime CompleteByDate { get; set; }
        public string SeatNumber { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}

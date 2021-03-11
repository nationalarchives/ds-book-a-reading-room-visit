namespace book_a_reading_room_visit.api.Models
{
    public class BookingResponseModel
    {
        public string BookingReference { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}

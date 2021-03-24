namespace book_a_reading_room_visit.model
{
    public class KewBookingSeatUpdateModel
    {
        public int BookingId { get; set; }

        public int NewSeatId { get; set; }

        public string Comment { get; set; }

        public string UpdatedBy { get; set; }
    }
}

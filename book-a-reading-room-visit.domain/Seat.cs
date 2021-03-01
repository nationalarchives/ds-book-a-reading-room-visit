namespace book_a_reading_room_visit.domain
{
    public class Seat
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int SeatTypeId { get; set; }
        public SeatType SeatType { get; set; }
    }
}

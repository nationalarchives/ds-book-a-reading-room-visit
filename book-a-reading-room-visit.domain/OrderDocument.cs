namespace book_a_reading_room_visit.domain
{
    public class OrderDocument
    {
        public int Id { get; set; }
        public string DocumentReference { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
        public string LetterCode { get; set; }
        public int ClassNumber { get; set; }
        public int PieceId { get; set; }
        public string PieceReference { get; set; }
        public int? SubClassNumber { get; set; }
        public string ItemReference { get; set; }
        public string Site { get; set; }
        public bool IsReserve { get; set; }
        public bool? Requisitioned { get; set; }
    }
}

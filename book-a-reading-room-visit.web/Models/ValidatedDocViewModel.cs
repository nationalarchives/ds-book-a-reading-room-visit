namespace book_a_reading_room_visit.web.Models
{
    public class ValidatedDocViewModel
    {
        public int PieceId { get; set; }
        public string DocumentReference { get; set; }
        public string DocumentDescription { get; set; }
        public bool DocumentIsOffsite { get; set; }
        public bool IsReserved { get; set; }
    }
}

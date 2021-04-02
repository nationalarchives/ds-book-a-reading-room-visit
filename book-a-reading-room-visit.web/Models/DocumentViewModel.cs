namespace book_a_reading_room_visit.web.Models
{
    public class DocumentViewModel
    {
        public int PieceId { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public bool IsOffsite { get; set; }
        public bool IsReserved { get; set; }
    }
}

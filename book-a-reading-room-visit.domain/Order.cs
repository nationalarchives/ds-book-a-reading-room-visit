using System;

namespace book_a_reading_room_visit.domain
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public string OrderReference { get; set; }
        public bool IsStandardVisit { get; set; }
        public Seat Seat { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ReaderTicket { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

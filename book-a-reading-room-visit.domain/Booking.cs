using System;

namespace book_a_reading_room_visit.domain
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string BookingReference { get; set; }
        public int BookingTypeId { get; set; }
        public BookingType BookingType { get; set; }
        public bool IsAcceptTsAndCs { get; set; }
        public bool IsAcceptCovidCharter { get; set; }
        public bool IsNoShow { get; set; }
        public int SeatId { get; set; }
        public Seat Seat { get; set; }
        public int BookingStatusId { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public string Comments { get; set; }
        public DateTime VisitStartDate { get; set; }
        public DateTime VisitEndDate { get; set; }
        public int? ReaderTicket { get; set; }
        public string AdditionalRequirements { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LastModifiedBy { get; set; }
    }
}

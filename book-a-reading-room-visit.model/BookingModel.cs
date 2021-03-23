using System;
using System.Collections.Generic;

namespace book_a_reading_room_visit.model
{
    public class BookingModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompleteByDate { get; set; }
        public string BookingReference { get; set; }
        public BookingTypes BookingType { get; set; }
        public bool IsAcceptTsAndCs { get; set; }
        public bool IsAcceptCovidCharter { get; set; }
        public bool IsNoFaceCovering { get; set; }
        public bool IsNoShow { get; set; }
        public int SeatId { get; set; }
        public SeatTypes SeatType { get; set; }
        public string SeatTypeDescription { get; set; }
        public string   SeatNumber { get; set; }
        public BookingStatuses BookingStatus { get; set; }
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
        public List<OrderDocumentModel> OrderDocuments { get; set; }
    }
}

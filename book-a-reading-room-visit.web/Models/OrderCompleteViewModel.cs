using book_a_reading_room_visit.model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace book_a_reading_room_visit.web.Models
{
    public class OrderCompleteViewModel
    {
        public BookingTypes BookingType { get; set; }
        public BookingStatuses BookingStatus { get; set; }
        public int? ReaderTicket { get; set; }
        public string BookingReference { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime? CompleteByDate { get; set; }
        public SeatTypes SeatType { get; set; }
        public string SeatNumber { get; set; }
        public string AdditionalRequirements { get; set; }
        public List<DocumentViewModel> Documents { get; set; }
        public int MinimumRequiredDocuments
        {
            get
            {
                int minDocuments = 0;
                if (BookingType == BookingTypes.StandardOrderVisit && Documents.Count(x => !x.IsReserved) == 0)
                {
                    minDocuments = 1;
                }
                if (BookingType == BookingTypes.BulkOrderVisit && Documents.Count < 20)
                {
                    minDocuments = 20 - Documents.Count;
                }
                return minDocuments;
            }
        }
    }
}

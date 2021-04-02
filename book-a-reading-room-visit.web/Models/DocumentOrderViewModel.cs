using book_a_reading_room_visit.model;
using System;
using System.Collections.Generic;

namespace book_a_reading_room_visit.web.Models
{
    public class DocumentOrderViewModel
    {
        public BookingTypes BookingType { get; set; }
        public int? ReaderTicket { get; set; }
        public string BookingReference { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime? CompleteByDate { get; set; }
        public SeatTypes SeatType { get; set; }
        public string SeatNumber { get; set; }
        public string Series { get; set; }
        public string AdditionalRequirements { get; set; }
        public string ReserveDocumentReference1 { get; set; }
        public string ReserveDocumentReference2 { get; set; }
        public string ReserveDocumentReference3 { get; set; }
        public string DocumentReference1 { get; set; }
        public string DocumentReference2 { get; set; }
        public string DocumentReference3 { get; set; }
        public string DocumentReference4 { get; set; }
        public string DocumentReference5 { get; set; }
        public string DocumentReference6 { get; set; }
        public string DocumentReference7 { get; set; }
        public string DocumentReference8 { get; set; }
        public string DocumentReference9 { get; set; }
        public string DocumentReference10 { get; set; }
        public string DocumentReference11 { get; set; }
        public string DocumentReference12 { get; set; }
        public string DocumentReference13 { get; set; }
        public string DocumentReference14 { get; set; }
        public string DocumentReference15 { get; set; }
        public string DocumentReference16 { get; set; }
        public string DocumentReference17 { get; set; }
        public string DocumentReference18 { get; set; }
        public string DocumentReference19 { get; set; }
        public string DocumentReference20 { get; set; }
        public string DocumentReference21 { get; set; }
        public string DocumentReference22 { get; set; }
        public string DocumentReference23 { get; set; }
        public string DocumentReference24 { get; set; }
        public string DocumentReference25 { get; set; }
        public string DocumentReference26 { get; set; }
        public string DocumentReference27 { get; set; }
        public string DocumentReference28 { get; set; }
        public string DocumentReference29 { get; set; }
        public string DocumentReference30 { get; set; }
        public string DocumentReference31 { get; set; }
        public string DocumentReference32 { get; set; }
        public string DocumentReference33 { get; set; }
        public string DocumentReference34 { get; set; }
        public string DocumentReference35 { get; set; }
        public string DocumentReference36 { get; set; }
        public string DocumentReference37 { get; set; }
        public string DocumentReference38 { get; set; }
        public string DocumentReference39 { get; set; }
        public string DocumentReference40 { get; set; }
        public int MinimumRequiredDocuments
        {
            get
            {
                int minDocuments = 0;
                if (BookingType == BookingTypes.StandardOrderVisit && string.IsNullOrWhiteSpace(DocumentReference1))
                { 
                    minDocuments = 1;
                }
                if (BookingType == BookingTypes.BulkOrderVisit)
                {
                    minDocuments += string.IsNullOrWhiteSpace(DocumentReference1) ? 1 : 0;
                    minDocuments += string.IsNullOrWhiteSpace(DocumentReference2) ? 1 : 0;
                    minDocuments += string.IsNullOrWhiteSpace(DocumentReference3) ? 1 : 0;
                    minDocuments += string.IsNullOrWhiteSpace(DocumentReference4) ? 1 : 0;
                    minDocuments += string.IsNullOrWhiteSpace(DocumentReference5) ? 1 : 0;
                    minDocuments += string.IsNullOrWhiteSpace(DocumentReference6) ? 1 : 0;
                    minDocuments += string.IsNullOrWhiteSpace(DocumentReference7) ? 1 : 0;
                    minDocuments += string.IsNullOrWhiteSpace(DocumentReference8) ? 1 : 0;
                    minDocuments += string.IsNullOrWhiteSpace(DocumentReference9) ? 1 : 0;
                    minDocuments += string.IsNullOrWhiteSpace(DocumentReference10) ? 1 : 0;
                    minDocuments += string.IsNullOrWhiteSpace(DocumentReference11) ? 1 : 0;
                    minDocuments += string.IsNullOrWhiteSpace(DocumentReference12) ? 1 : 0;
                    minDocuments += string.IsNullOrWhiteSpace(DocumentReference13) ? 1 : 0;
                    minDocuments += string.IsNullOrWhiteSpace(DocumentReference14) ? 1 : 0;
                    minDocuments += string.IsNullOrWhiteSpace(DocumentReference15) ? 1 : 0;
                    minDocuments += string.IsNullOrWhiteSpace(DocumentReference16) ? 1 : 0;
                    minDocuments += string.IsNullOrWhiteSpace(DocumentReference17) ? 1 : 0;
                    minDocuments += string.IsNullOrWhiteSpace(DocumentReference18) ? 1 : 0;
                    minDocuments += string.IsNullOrWhiteSpace(DocumentReference19) ? 1 : 0;
                    minDocuments += string.IsNullOrWhiteSpace(DocumentReference20) ? 1 : 0;
                }
                return minDocuments;
            }
        }
    }
}

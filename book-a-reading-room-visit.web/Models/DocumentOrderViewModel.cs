using book_a_reading_room_visit.model;
using book_a_reading_room_visit.web.Helper;
using System;
using System.ComponentModel.DataAnnotations;

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
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string Series { get; set; }
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string AdditionalRequirements { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string ReserveDocumentReference1 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string ReserveDocumentReference2 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string ReserveDocumentReference3 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference1 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference2 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference3 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference4 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference5 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference6 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference7 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference8 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference9 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference10 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference11 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference12 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference13 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference14 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference15 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference16 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference17 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference18 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference19 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference20 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference21 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference22 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference23 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference24 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference25 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference26 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference27 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference28 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference29 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference30 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference31 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference32 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference33 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference34 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference35 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference36 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference37 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference38 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string DocumentReference39 { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
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

        public bool HaveNoDocumentReference
        {
            get
            {
                return (string.IsNullOrWhiteSpace(DocumentReference1) && string.IsNullOrWhiteSpace(DocumentReference2) &&
                        string.IsNullOrWhiteSpace(DocumentReference3) && string.IsNullOrWhiteSpace(DocumentReference4) &&
                        string.IsNullOrWhiteSpace(DocumentReference5) && string.IsNullOrWhiteSpace(DocumentReference6) &&
                        string.IsNullOrWhiteSpace(DocumentReference7) && string.IsNullOrWhiteSpace(DocumentReference8) &&
                        string.IsNullOrWhiteSpace(DocumentReference9) && string.IsNullOrWhiteSpace(DocumentReference10) &&
                        string.IsNullOrWhiteSpace(DocumentReference11) && string.IsNullOrWhiteSpace(DocumentReference12) &&
                        string.IsNullOrWhiteSpace(DocumentReference13) && string.IsNullOrWhiteSpace(DocumentReference14) &&
                        string.IsNullOrWhiteSpace(DocumentReference15) && string.IsNullOrWhiteSpace(DocumentReference16) &&
                        string.IsNullOrWhiteSpace(DocumentReference17) && string.IsNullOrWhiteSpace(DocumentReference18) &&
                        string.IsNullOrWhiteSpace(DocumentReference19) && string.IsNullOrWhiteSpace(DocumentReference20) &&
                        string.IsNullOrWhiteSpace(DocumentReference21) && string.IsNullOrWhiteSpace(DocumentReference22) &&
                        string.IsNullOrWhiteSpace(DocumentReference23) && string.IsNullOrWhiteSpace(DocumentReference24) &&
                        string.IsNullOrWhiteSpace(DocumentReference25) && string.IsNullOrWhiteSpace(DocumentReference26) &&
                        string.IsNullOrWhiteSpace(DocumentReference27) && string.IsNullOrWhiteSpace(DocumentReference28) &&
                        string.IsNullOrWhiteSpace(DocumentReference29) && string.IsNullOrWhiteSpace(DocumentReference30) &&
                        string.IsNullOrWhiteSpace(DocumentReference31) && string.IsNullOrWhiteSpace(DocumentReference32) &&
                        string.IsNullOrWhiteSpace(DocumentReference33) && string.IsNullOrWhiteSpace(DocumentReference34) &&
                        string.IsNullOrWhiteSpace(DocumentReference35) && string.IsNullOrWhiteSpace(DocumentReference36) &&
                        string.IsNullOrWhiteSpace(DocumentReference37) && string.IsNullOrWhiteSpace(DocumentReference38) &&
                        string.IsNullOrWhiteSpace(DocumentReference39) && string.IsNullOrWhiteSpace(DocumentReference40));
            }
        }
    }
}

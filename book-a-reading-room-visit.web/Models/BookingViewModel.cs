using book_a_reading_room_visit.model;
using book_a_reading_room_visit.web.Helper;
using System;
using System.ComponentModel.DataAnnotations;

namespace book_a_reading_room_visit.web.Models
{
    public class BookingViewModel
    {
        public string BookingReference { get; set; }
        public DateTime ExpiredBy { get; set; }
        public string SeatNumber { get; set; }
        public BookingTypes BookingType { get; set; }
        public SeatTypes SeatType { get; set; }
        public DateTime? CompleteByDate { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
        [Range(typeof(bool), "true", "true", ErrorMessage = Constants.Accept_Terms_Privacy_Required)]
        public bool AcceptTsAndCs { get; set; }
        [Range(typeof(bool), "true", "true", ErrorMessage = Constants.Accept_COVID_19_Required)]
        public bool AcceptCovidCharter { get; set; }
        public bool NoFaceCovering { get; set; }
        [Required(ErrorMessage = Constants.Valid_Ticket_Required)]
        public string Ticket { get; set; }

        public int ReaderTicket { 
            get
            {
                if (int.TryParse(Ticket?.ToLower()?.Replace("t", "-"), out int ticket))
                {
                    return ticket;
                }
                return 0;
            }
        }
        [EmailAddress(ErrorMessage = Constants.Valid_Email_Required)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string Email { get; set; }
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string Phone { get; set; }
        [Required(ErrorMessage = Constants.Firstname_Required)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = Constants.Lastname_Required)]
        [RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = Constants.Html_Tags_Not_Allowed)]
        public string LastName { get; set; }
    }
}

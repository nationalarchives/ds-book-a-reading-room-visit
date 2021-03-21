using book_a_reading_room_visit.model;
using System;
using System.ComponentModel.DataAnnotations;

namespace book_a_reading_room_visit.web.Models
{
    public class BookingViewModel
    {
        public string BookingReference { get; set; }
        public string SeatNumber { get; set; }
        public BookingTypes BookingType { get; set; }
        public SeatTypes SeatType { get; set; }
        public DateTime CompleteByDate { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
        public bool AcceptTsAndCs { get; set; }
        public bool AcceptCovidCharter { get; set; }
        public bool NoFaceCovering { get; set; }
        public string Ticket { get; set; }

        public int ReadingTicket { 
            get
            {
                if (int.TryParse(Ticket?.ToLower()?.Replace("t", "-"), out int ticket))
                {
                    return ticket;
                }
                return 0;
            }
        }
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

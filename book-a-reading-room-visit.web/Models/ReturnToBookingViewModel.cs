using book_a_reading_room_visit.web.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.web.Models
{
    public class ReturnToBookingViewModel
    {
        [Required(ErrorMessage = Constants.Valid_Ticket_Required)]
        [MaxLength(11)]
        public string Ticket { get; set; }

        [Required(ErrorMessage = Constants.Valid_BookingReference_Required)]
        [MaxLength(50)]
        public string BookingReference { get; set; }

        public int ReaderTicket
        {
            get
            {
                if (int.TryParse(Ticket?.ToLower()?.Replace("t", "-"), out int ticket))
                {
                    return ticket;
                }
                return 0;
            }
        }
    }
}

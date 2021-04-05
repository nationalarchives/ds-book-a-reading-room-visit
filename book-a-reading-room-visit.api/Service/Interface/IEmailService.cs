using book_a_reading_room_visit.model;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Service
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailType emailType, string toAddress, BookingModel bookingModel);
    }
}

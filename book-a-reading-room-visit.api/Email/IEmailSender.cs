using book_a_reading_room_visit.model;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Email
{
    public interface IEmailSender
    {
        Task SendEmail(string from, string to, string subject, string textBody, string htmlBody, EmailHeader[] additionalHeaders = null);
    }
}

        
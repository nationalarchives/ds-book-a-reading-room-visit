using book_a_reading_room_visit.model;

namespace book_a_reading_room_visit.api.Service
{
    public class EmailServiceDummy : IEmailService
    {
        public Task SendEmailAsync(EmailType emailType, string toAddress, BookingModel bookingModel)
        {
            Console.WriteLine($"Sending booking email of type [{emailType.ToString()}] to {toAddress}");
            return Task.CompletedTask;
        }
    }
}

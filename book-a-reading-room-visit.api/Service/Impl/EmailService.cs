using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Service
{
    public class EmailService : IEmailService
    {
        private IAmazonSimpleEmailService _amazonSimpleEmailService;
        // The email body for recipients with non-HTML email clients.
        static readonly string textBody = "Amazon SES Test (.NET)\r\n"
                                        + "This email was sent through Amazon SES "
                                        + "using the AWS SDK for .NET.";

        // The HTML body of the email.
        static readonly string htmlBody = @"<html>
                                            <head></head>
                                            <body>
                                              <h1>Amazon SES Test (AWS SDK for .NET)</h1>
                                              <p>This email was sent with
                                                <a href='https://aws.amazon.com/ses/'>Amazon SES</a> using the
                                                <a href='https://aws.amazon.com/sdk-for-net/'>
                                                  AWS SDK for .NET</a>.</p>
                                            </body>
                                            </html>";
        public EmailService(IAmazonSimpleEmailService amazonSimpleEmailService)
        {
            _amazonSimpleEmailService = amazonSimpleEmailService;
        }
        public async Task SendEmail(string from, string to)
        {
            var mail = CreateMailMessage(from, to);

            await _amazonSimpleEmailService.SendEmailAsync(mail);
        }

        private SendEmailRequest CreateMailMessage(string from, string to)
        {
            var sendRequest = new SendEmailRequest
            {
                Source = from,
                Destination = new Destination
                {
                    ToAddresses =
                           new List<string> { to }
                },
                Message = new Message
                {
                    Subject = new Content("KBS-Test-Email"),
                    Body = new Body
                    {
                        Html = new Content
                        {
                            Charset = "UTF-8",
                            Data = htmlBody
                        },
                        Text = new Content
                        {
                            Charset = "UTF-8",
                            Data = textBody
                        }
                    }

                },
                // If you are not using a configuration set, comment
                // or remove the following line 
                //ConfigurationSetName = configSet
            };
            return sendRequest;
        }
    }
}

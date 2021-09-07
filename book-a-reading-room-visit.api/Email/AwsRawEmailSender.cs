using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Email
{
    public class AwsRawEmailSender : IEmailSender
    {

        private readonly IAmazonSimpleEmailService _amazonSimpleEmailService;

        public AwsRawEmailSender(IAmazonSimpleEmailService amazonSimpleEmailService)
        {
            _amazonSimpleEmailService = amazonSimpleEmailService;
        }

        public async Task SendEmail(string from, string to, string subject, string textBody, string htmlBody)
        {
            MemoryStream messageStream = BuildMessageStream(from, to, subject, textBody, htmlBody);

            var rawMessage = new RawMessage(messageStream);
            var rawEmailRequest = new SendRawEmailRequest(rawMessage);

            await _amazonSimpleEmailService.SendRawEmailAsync(rawEmailRequest);
        }

        private  MemoryStream BuildMessageStream(string from, string to, string subject, string textBody, string htmlBody)
        {
            string senderDisplayname = from.Substring(0, from.IndexOf("<") - 1);
            string senderEmail = from.Substring(from.IndexOf("<"));

            var sb = new StringBuilder($"From: \"{senderDisplayname}\" {senderEmail}");
            sb.Append("\n");
            sb.Append($"To: {to}");
            sb.Append("\n");
            sb.Append($"Subject: {subject}");
            sb.Append("\n");
            sb.Append("X-SES-CONFIGURATION-SET: KBSEmailsToDSD");
            sb.Append("\n");
            sb.Append("X-SES-MESSAGE-TAGS: booking-confirmation=adv-order");
            sb.Append("\n");
            sb.Append("Content-Type: multipart/alternative;boundary=\"Multipart_687cbcb1065148178784dc4ea27d7cd6\"");
            sb.Append("\n\n");
            sb.Append("--Multipart_687cbcb1065148178784dc4ea27d7cd6");
            sb.Append("\n");
            sb.Append("Content-Type: text/plain; charset=iso-8859-1");
            sb.Append("\n");
            sb.Append("Content-Transfer-Encoding: quoted-printable");
            
            sb.Append("\n\n");
            sb.Append(textBody);
            sb.Append("\n\n");

            sb.Append("--Multipart_687cbcb1065148178784dc4ea27d7cd6");
            sb.Append("\n");
            //sb.Append("Content-Type: text/html; charset=iso-8859-1");
            sb.Append("Content-Type: text/html; charset=UTF-8");
            sb.Append("\n");
            sb.Append("Content-Transfer-Encoding: quoted-printable");
            
            sb.Append("\n\n");
            sb.Append(htmlBody);
            sb.Append("\n\n");

            sb.Append("--Multipart_687cbcb1065148178784dc4ea27d7cd6--");

            Debug.Print(sb.ToString());

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString()));

            return stream;
        }
    }
}

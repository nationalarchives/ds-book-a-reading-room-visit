using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Helper
{
    public static class EmailHelper
    {
        public static MemoryStream MessageStream(string from, string to, string subject, string textBody, string htmlBody)
        {
            var sb = new StringBuilder($"From: {from}\nTo: {to}\n");
            sb.Append($"Subject: {subject}\n");
            sb.Append($"multipart/mixed;boundary = \"a3f166a86b56ff6c37755292d690675717ea3cd9de81228ec2b76ed4a15d6d1a\"n");
            sb.Append("\n");
            sb.Append("--a3f166a86b56ff6c37755292d690675717ea3cd9de81228ec2b76ed4a15d6d1a");
            sb.Append("X-SES-CONFIGURATION-SET: KBSEmailsToDSD");
            sb.Append("X-SES-MESSAGE-TAGS: booking - confirmation = adv - order");
            sb.Append("Content-Type: multipart/alternative;boundary = \"sub_a3f166a86b56ff6c37755292d690675717ea3cd9de81228ec2b76ed4a15d6d1a\"");
            sb.Append("--sub_a3f166a86b56ff6c37755292d690675717ea3cd9de81228ec2b76ed4a15d6d1a");
            sb.Append("Content - Type: text / plain; charset = iso - 8859 - 1");
            sb.Append("Content - Transfer - Encoding: quoted - printable");

            sb.Append(textBody);

            sb.Append("--sub_a3f166a86b56ff6c37755292d690675717ea3cd9de81228ec2b76ed4a15d6d1asb.");
            sb.Append("Content - Type: text / html; charset = iso - 8859 - 1");
            sb.Append("Content - Transfer - Encoding: quoted - printable");

            sb.Append(htmlBody);

            sb.Append("-sub_a3f166a86b56ff6c37755292d690675717ea3cd9de81228ec2b76ed4a15d6d1a--");

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString()));

            return stream;
        }
    }
}

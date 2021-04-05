using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using book_a_reading_room_visit.model;
using Microsoft.Extensions.Configuration;

namespace book_a_reading_room_visit.api.Service
{
    public class EmailService : IEmailService
    {
        private readonly IAmazonSimpleEmailService _amazonSimpleEmailService;
        private readonly IConfiguration _configuration;

        public EmailService(IAmazonSimpleEmailService amazonSimpleEmailService, IConfiguration configuration)
        {
            _amazonSimpleEmailService = amazonSimpleEmailService;
            _configuration = configuration;
        }
        public async Task SendEmailAsync(EmailType emailType, string toAddress, BookingModel bookingModel)
        {
            var fromAddress = _configuration.GetValue<string>("EmailSettings:FromAddress");
            var subject = string.Empty;
            switch (emailType)
            {
                case EmailType.ReservationConfirmation:
                    {
                        var subjectFormat = _configuration.GetValue<string>("EmailSettings:ReservationSubject");
                        subject = string.Format(subjectFormat, $"{bookingModel.VisitStartDate:dddd dd MMMM yyyy}");
                        break;
                    }
                case EmailType.BookingConfirmation:
                    {
                        var subjectFormat = _configuration.GetValue<string>("EmailSettings:ConfirmationSubject");
                        subject = string.Format(subjectFormat, $"{bookingModel.VisitStartDate:dddd dd MMMM yyyy}", 
                                                                bookingModel.BookingType == BookingTypes.StandardOrderVisit ? "standard" : "bulk");
                        break;
                    }
                case EmailType.BookingCancellation:
                    {
                        subject = _configuration.GetValue<string>("EmailSettings:CancelSubject");
                        break;
                    }
            }

            var xDocument = GetXDocument(bookingModel);
            var htmlBody = GetHtmlBody(emailType, xDocument);
            var sendRequest = new SendEmailRequest
            {
                Source = fromAddress,
                Destination = new Destination
                {
                    ToAddresses =
                           new List<string> { toAddress }
                },
                Message = new Message
                {
                    Subject = new Content(subject),
                    Body = new Body
                    {
                        Html = new Content
                        {
                            Charset = "UTF-8",
                            Data = htmlBody
                        }
                    }

                }
            };

            await _amazonSimpleEmailService.SendEmailAsync(sendRequest);
        }

        private string GetHtmlBody(EmailType emailType, XDocument xDocument)
        {
            try
            {
                var fileName = $"EmailTemplate/{emailType}.xslt";
                var filePath = Path.GetFullPath(fileName);
                XslCompiledTransform xslTransform = new XslCompiledTransform();
                xslTransform.Load(filePath);

                var xmlDocument = new XmlDocument();
                using (var xmlReader = xDocument.CreateReader())
                {
                    xmlDocument.Load(xmlReader);
                }

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    xslTransform.Transform(new XmlNodeReader(xmlDocument), null, memoryStream);
                    memoryStream.Position = 0;
                    StreamReader streamReader = new StreamReader(memoryStream, Encoding.UTF8);
                    return streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "";
        }

        private XDocument GetXDocument(BookingModel bookingModel)
        {
            var returnURL = Environment.GetEnvironmentVariable("ReturnURL");
            var rootElement = new XElement("Root");
            rootElement.Add(new XElement("Name", $"{bookingModel.FirstName} {bookingModel.LastName}"));
            rootElement.Add(new XElement("CompleteByDate", $"{bookingModel.CompleteByDate:dddd dd MMMM yyyy} at {bookingModel.CompleteByDate:hh:mm tt}"));
            rootElement.Add(new XElement("BookingReference", bookingModel.BookingReference));
            rootElement.Add(new XElement("ReaderTicket", bookingModel.ReaderTicket));
            rootElement.Add(new XElement("VisitType", bookingModel.BookingType == BookingTypes.StandardOrderVisit ? "Standard visit" : "Bulk order visit"));
            rootElement.Add(new XElement("VisitStartDate", $"{bookingModel.VisitStartDate:dddd dd MMMM yyyy}"));
            rootElement.Add(new XElement("SeatNumber", bookingModel.SeatNumber));
            rootElement.Add(new XElement("AdditionalRequirements", bookingModel.AdditionalRequirements));
            rootElement.Add(new XElement("ReturnURL", returnURL));

            var documentCount = 1;
            foreach (var document in bookingModel.OrderDocuments.Where(d => !d.IsReserve).ToList())
            {
                var documentOrder = new XElement("DocumentOrder");
                documentOrder.Add(new XElement("Label", $"Document {documentCount}: "));
                documentOrder.Add(new XElement("Document", $"{document.DocumentReference}: {document.Description}"));
                documentCount += 1;
                rootElement.Add(documentOrder);
            }
            documentCount = 1;
            foreach (var document in bookingModel.OrderDocuments.Where(d => d.IsReserve).ToList())
            {
                var reserveDocumentOrder = new XElement("ReserveDocumentOrder");
                reserveDocumentOrder.Add(new XElement("Label", $"Reserve document {documentCount}: "));
                reserveDocumentOrder.Add(new XElement("Document", $"{document.DocumentReference}: {document.Description}"));
                documentCount += 1;
                rootElement.Add(reserveDocumentOrder);
            }
            return new XDocument(rootElement);
        }
    }
}

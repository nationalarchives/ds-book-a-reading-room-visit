﻿using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using book_a_reading_room_visit.model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

[assembly: InternalsVisibleTo("book-a-reading-room-visit.test")]
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
                        subject = string.Format(subjectFormat, $"{bookingModel.VisitStartDate:dddd dd MMMM yyyy}");
                        //subject = string.Format(subjectFormat, $"{bookingModel.VisitStartDate:dddd dd MMMM yyyy}", bookingModel.BookingType == BookingTypes.StandardOrderVisit ? "standard" : "bulk");
                        break;
                    }
                case EmailType.BookingCancellation:
                    {
                        subject = _configuration.GetValue<string>("EmailSettings:CancelSubject");
                        break;
                    }
                case EmailType.AutomaticCancellation:
                    {
                        subject = _configuration.GetValue<string>("EmailSettings:AutoCancelSubject");
                        break;
                    }
                case EmailType.ValidOrderReminder:
                    {
                        subject = _configuration.GetValue<string>("EmailSettings:ValidReminderSubject");
                        break;
                    }
                case EmailType.InvalidOrderReminder:
                    {
                        subject = _configuration.GetValue<string>("EmailSettings:InvalidReminderSubject");
                        break;
                    }
                case EmailType.DSDBookingConfirmation:
                    {
                        fromAddress = _configuration.GetValue<string>("EmailSettings:DSDFromAddress");
                        subject = bookingModel.BookingType == BookingTypes.StandardOrderVisit ? $"{bookingModel.LastName} - Standard visit - {bookingModel.VisitStartDate:dddd dd MMMM yyyy}"
                                                                                              : $"{bookingModel.LastName} - Bulk order visit - {bookingModel.VisitStartDate:dddd dd MMMM yyyy}";
                        break;
                    }
                case EmailType.PostVisit:
                    {
                        var subjectFormat = _configuration.GetValue<string>("EmailSettings:PostVisitSubject");
                        subject = string.Format(subjectFormat, $"{bookingModel.VisitStartDate:dddd dd MMMM yyyy}");
                        break;
                    }
                case EmailType.InValidBulkOrderReminder:
                    {
                        subject = _configuration.GetValue<string>("EmailSettings:InValidBulkOrderReminderSubject");
                        break;
                    }
            }

            var xDocument = emailType == EmailType.DSDBookingConfirmation ? GetDSDXDocument(bookingModel) :  GetXDocument(bookingModel);
            var htmlBody = GetHtmlBody(emailType, xDocument);
            var textBody = GetTextBody(emailType, bookingModel);
            var sendRequest = new SendEmailRequest
            {
                Source = fromAddress,
                Destination = new Destination
                {
                    ToAddresses = toAddress.Split(',').ToList()
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
                        },
                        Text = new Content
                        {
                            Charset = "UTF-8",
                            Data = textBody
                        }
                    }

                }
            };
            await _amazonSimpleEmailService.SendEmailAsync(sendRequest);
        }

        internal string GetTextBody(EmailType emailType, BookingModel bookingModel)
        {
            var sb = new StringBuilder(File.ReadAllText($"EmailTemplate/Text/{emailType}.txt"));

            dynamic expando = new ExpandoObject();

            var dictionary = (IDictionary<string, object>)expando;

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(typeof(BookingModel)))
            {
                try
                {
                    if (property.PropertyType != typeof(List<string>))
                    {
                        dictionary.Add(property.Name, Convert.ToString(property.GetValue(bookingModel))); 
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            expando.ReaderTicket = bookingModel.ReaderTicket < 0 ? $"T{bookingModel.ReaderTicket*-1}" : $"{bookingModel.ReaderTicket}";
            expando.HomeURL = Environment.GetEnvironmentVariable("HomeURL");
            expando.ReturnURL = $"{expando.HomeURL}/return-to-booking";
            expando.VisitType = bookingModel.BookingType == BookingTypes.StandardOrderVisit ? "Standard visit" : "Bulk order visit";
            expando.Name = $"{bookingModel.FirstName} {bookingModel.LastName}";
            expando.VisitStartDateDisplay = bookingModel.VisitStartDate.ToShortDateString();
            expando.AdditionalRequirements = bookingModel.AdditionalRequirements ?? "None entered.";
            expando.Phone = bookingModel.Phone ?? "None entered.";

            if (bookingModel.BookingType == BookingTypes.StandardOrderVisit)
            {
                expando.ReadingRoom = bookingModel.SeatType == SeatTypes.StdRRSeat ? "Document reading room" : "Map and large document reading room";
            }

            foreach(KeyValuePair<string, object> kv in dictionary)
            {
                sb = sb.Replace("{" + kv.Key + "}", (kv.Value != null ? kv.Value.ToString() : string.Empty));
            }

            if(emailType  == EmailType.BookingConfirmation || emailType == EmailType.DSDBookingConfirmation || emailType == EmailType.ValidOrderReminder)
            {
                var orderDocuments = new StringBuilder();
                var documentCount = 0;
                foreach (var document in bookingModel.OrderDocuments.Where(d => !d.IsReserve).ToList())
                {
                    documentCount += 1;
                    if (emailType == EmailType.DSDBookingConfirmation)
                    {
                        orderDocuments.AppendFormat("Document {0}: {1}", documentCount, document.DocumentReference);
                    }
                    else
                    {
                        orderDocuments.AppendFormat("Document {0}: {1}: {2}", documentCount, document.DocumentReference, document.Description);
                    }
                }
                var reserveDocumentCount = 0;
                foreach (var document in bookingModel.OrderDocuments.Where(d => d.IsReserve).ToList())
                {
                    reserveDocumentCount += 1;
                    if (emailType == EmailType.DSDBookingConfirmation)
                    {
                        orderDocuments.AppendFormat("Reserve document {0}: {1}", reserveDocumentCount, document.DocumentReference);
                    }
                    else
                    {
                        orderDocuments.AppendFormat("Reserve document {0}: {1}: {2}", reserveDocumentCount, document.DocumentReference, document.Description);
                    }
                }

                sb = sb.Replace("{Order-Documents}", orderDocuments.ToString());
            }

            return sb.ToString();
        }

        private string GetHtmlBody(EmailType emailType, XDocument xDocument)
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
                string content = null;
                using (StreamReader streamReader = new StreamReader(memoryStream, Encoding.UTF8))
                {
                    content = streamReader.ReadToEnd();
                }
                return content;
            }
        }

        private XDocument GetXDocument(BookingModel bookingModel)
        {
            var homeURL = Environment.GetEnvironmentVariable("HomeURL");
            var rootElement = new XElement("Root");
            rootElement.Add(new XElement("Name", $"{bookingModel.FirstName} {bookingModel.LastName}"));
            rootElement.Add(new XElement("Phone", bookingModel.Phone ?? "None entered."));
            rootElement.Add(new XElement("CompleteByDate", $"{bookingModel.CompleteByDate:dddd dd MMMM yyyy}"));
            rootElement.Add(new XElement("BookingReference", bookingModel.BookingReference));
            rootElement.Add(new XElement("ReaderTicket", bookingModel.ReaderTicket < 0 ? $"T{bookingModel.ReaderTicket * -1}" : $"{bookingModel.ReaderTicket}"));
            rootElement.Add(new XElement("VisitType", bookingModel.BookingType == BookingTypes.StandardOrderVisit ? "Standard visit" : "Bulk order visit"));
            rootElement.Add(new XElement("VisitStartDate", $"{bookingModel.VisitStartDate:dddd dd MMMM yyyy}"));
            rootElement.Add(new XElement("SeatNumber", bookingModel.SeatNumber));
            rootElement.Add(new XElement("AdditionalRequirements", bookingModel.AdditionalRequirements ?? "None entered."));
            rootElement.Add(new XElement("ReturnURL", $"{homeURL}/return-to-booking"));
            rootElement.Add(new XElement("HomeURL", homeURL));
            if (bookingModel.BookingType == BookingTypes.StandardOrderVisit)
            {
                var readingRoom = bookingModel.SeatType == SeatTypes.StdRRSeat ? "Document reading room" : "Map and large document reading room";
                rootElement.Add(new XElement("ReadingRoom", readingRoom));
            }

            var documentCount = 0;
            foreach (var document in bookingModel.OrderDocuments.Where(d => !d.IsReserve).ToList())
            {
                documentCount += 1;
                var documentOrder = new XElement("DocumentOrder");
                documentOrder.Add(new XElement("Label", $"Document {documentCount}: "));
                documentOrder.Add(new XElement("Reference", document.DocumentReference));
                documentOrder.Add(new XElement("Description", document.Description));
                rootElement.Add(documentOrder);
            }
            var reserveDocumentCount = 0;
            foreach (var document in bookingModel.OrderDocuments.Where(d => d.IsReserve).ToList())
            {
                reserveDocumentCount += 1;
                var reserveDocumentOrder = new XElement("ReserveDocumentOrder");
                reserveDocumentOrder.Add(new XElement("Label", $"Reserve document {reserveDocumentCount}: "));
                reserveDocumentOrder.Add(new XElement("Reference", document.DocumentReference));
                reserveDocumentOrder.Add(new XElement("Description", document.Description));
                rootElement.Add(reserveDocumentOrder);
            }
            return new XDocument(rootElement);
        }

        private XDocument GetDSDXDocument(BookingModel bookingModel)
        {
            var homeURL = Environment.GetEnvironmentVariable("HomeURL");
            var rootElement = new XElement("Root");
            rootElement.Add(new XElement("Name", $"{bookingModel.FirstName} {bookingModel.LastName}"));
            rootElement.Add(new XElement("Phone", bookingModel.Phone));
            rootElement.Add(new XElement("CompleteByDate", $"{bookingModel.CompleteByDate:dddd dd MMMM yyyy}"));
            rootElement.Add(new XElement("BookingReference", bookingModel.BookingReference));
            rootElement.Add(new XElement("ReaderTicket", bookingModel.ReaderTicket < 0 ? $"T{bookingModel.ReaderTicket * -1}" : $"{bookingModel.ReaderTicket}"));
            rootElement.Add(new XElement("VisitType", bookingModel.BookingType == BookingTypes.StandardOrderVisit ? "Standard visit" : "Bulk order visit"));
            rootElement.Add(new XElement("VisitStartDate", $"{bookingModel.VisitStartDate:dddd dd MMMM yyyy}"));
            rootElement.Add(new XElement("SeatNumber", bookingModel.SeatNumber));
            rootElement.Add(new XElement("AdditionalRequirements", bookingModel.AdditionalRequirements));
            rootElement.Add(new XElement("ReturnURL", $"{homeURL}/return-to-booking"));
            rootElement.Add(new XElement("HomeURL", homeURL));

            if (bookingModel.BookingType == BookingTypes.StandardOrderVisit)
            {
                var readingRoom = bookingModel.SeatType == SeatTypes.StdRRSeat ? "Document reading room" : "Map and large document reading room";
                rootElement.Add(new XElement("ReadingRoom", readingRoom));
            }

            var documentCount = 0;
            var totalDocuments = bookingModel.OrderDocuments.Count(d => !d.IsReserve) / 2 + bookingModel.OrderDocuments.Count(d => !d.IsReserve) % 2;
            foreach (var document in bookingModel.OrderDocuments.Where(d => !d.IsReserve).ToList())
            {
                documentCount += 1;
                if (documentCount > totalDocuments)
                {
                    var documentOrder = new XElement("DocumentOrder2");
                    documentOrder.Add(new XElement("Label", $"Document {documentCount}: "));
                    documentOrder.Add(new XElement("Reference", document.DocumentReference));
                    documentOrder.Add(new XElement("Description", document.Description));
                    rootElement.Add(documentOrder);
                }
                else
                {
                    var documentOrder = new XElement("DocumentOrder1");
                    documentOrder.Add(new XElement("Label", $"Document {documentCount}: "));
                    documentOrder.Add(new XElement("Reference", document.DocumentReference));
                    documentOrder.Add(new XElement("Description", document.Description));
                    rootElement.Add(documentOrder);
                }
            }
            var reserveDocumentCount = 0;
            foreach (var document in bookingModel.OrderDocuments.Where(d => d.IsReserve).ToList())
            {
                reserveDocumentCount += 1;
                var reserveDocumentOrder = new XElement("ReserveDocumentOrder");
                reserveDocumentOrder.Add(new XElement("Label", $"Reserve {reserveDocumentCount}: "));
                reserveDocumentOrder.Add(new XElement("Reference", document.DocumentReference));
                rootElement.Add(reserveDocumentOrder);
            }
            return new XDocument(rootElement);
        }
    }
}

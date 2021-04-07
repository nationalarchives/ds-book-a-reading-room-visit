using Amazon.SimpleEmail;
using book_a_reading_room_visit.api.Service;
using book_a_reading_room_visit.domain;
using book_a_reading_room_visit.model;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.test
{
    [TestClass]
    public class EmailUnitTest
    {
        private IAmazonSimpleEmailService _awsSes;
        private IConfiguration _config;

        public EmailUnitTest()
        {
            _awsSes = Substitute.For<IAmazonSimpleEmailService>();
            _config = Substitute.For<IConfiguration>();
            

            var config = new ConfigurationBuilder().AddEnvironmentVariables().Build();
        }

        [TestMethod]
        public async Task Email_GetText_BookingConfirmation()
        {
            BookingModel bookingModel = CreateBooking(BookingStatuses.Created, true);

            var emailService = new EmailService(_awsSes, _config);

            string emailText = emailService.GetTextBody(EmailType.BookingConfirmation, bookingModel);

            string visitType =  bookingModel.BookingType == BookingTypes.StandardOrderVisit ? "Standard visit" : "Bulk order visit";

            Assert.IsTrue(emailText.IndexOf(bookingModel.BookingReference) != -1);
            Assert.IsTrue(emailText.IndexOf($"{bookingModel.FirstName} {bookingModel.LastName }") != -1);
            Assert.IsTrue(emailText.IndexOf(Convert.ToString(bookingModel.ReaderTicket.Value)) != -1);
            Assert.IsTrue(emailText.IndexOf(bookingModel.Phone) != -1);
            Assert.IsTrue(emailText.IndexOf(visitType) != -1);
            Assert.IsTrue(emailText.IndexOf("Standard Reading Room") != -1);

            if(!String.IsNullOrWhiteSpace(bookingModel.AdditionalRequirements))
            {
                Assert.IsTrue(emailText.IndexOf(bookingModel.AdditionalRequirements) != -1);
            }
            

            CheckDocumentOrders(bookingModel, emailText);

            Assert.IsTrue(emailText.IndexOf(bookingModel.VisitStartDate.ToShortDateString()) > -1);

        }

        [TestMethod]
        public async Task Email_GetText_DSDBookingConfirmation()
        {
            BookingModel bookingModel = CreateBooking(BookingStatuses.Created, true);

            var emailService = new EmailService(_awsSes, _config);

            string emailText = emailService.GetTextBody(EmailType.BookingConfirmation, bookingModel);

            Assert.IsTrue(emailText.IndexOf(bookingModel.BookingReference) != -1);
            Assert.IsTrue(emailText.IndexOf($"{bookingModel.FirstName} {bookingModel.LastName }") != -1);
            Assert.IsTrue(emailText.IndexOf(Convert.ToString(bookingModel.ReaderTicket.Value)) != -1);
            Assert.IsTrue(emailText.IndexOf(bookingModel.Phone) != -1);

            Assert.IsTrue(emailText.IndexOf(bookingModel.VisitStartDate.ToShortDateString()) > -1);

            CheckDocumentOrders(bookingModel, emailText);

        }
        private BookingModel CreateBooking(BookingStatuses bookingStatus, bool addOrderDocuments)
        {
            var bookingModel = new BookingModel()
            {
                Id = 12345,
                BookingReference = "RR982-720-1028J",
                BookingStatus = bookingStatus,
                BookingType = BookingTypes.StandardOrderVisit,
                AdditionalRequirements = "Hobbit size seat",
                CreatedDate = DateTime.Now.Date,
                CompleteByDate = DateTime.Now.Date.AddDays(10),
                VisitStartDate = DateTime.Now.Date.AddDays(20),
                BookingTypeDescription = "Standard Booking",
                FirstName = "Bilbo",
                LastName = "Baggins",
                Email = "bilbo.baggins@nationalarchives.gov.uk",
                Phone = "+44(0)123 567 789",
                ReaderTicket = 9497920                 
            };

            if (addOrderDocuments)
            {
                bookingModel.OrderDocuments = new List<OrderDocumentModel>();

                bookingModel.OrderDocuments.Add(new OrderDocumentModel { DocumentReference = "BT 51/6/9730", Description = "Pattern of Lace Curtain Design.", IsReserve = false });
                bookingModel.OrderDocuments.Add(new OrderDocumentModel { DocumentReference = "AIR 37/1171", Description = "SHAEF (MAIN AND REAR): Air signals, Transport Command.", IsReserve = false });
                bookingModel.OrderDocuments.Add(new OrderDocumentModel { DocumentReference = "C 13/953/44", Description = "Plaintiffs: Thomas Jeffries and another. Defendants: James Price Dale and George Dale.", IsReserve = false });

                bookingModel.OrderDocuments.Add(new OrderDocumentModel { DocumentReference = "C 13/953/30", Description = "Delincourt v Delincourt.", IsReserve = true });
            }

            return bookingModel;
        }

        private static void CheckDocumentOrders(BookingModel bookingModel, string emailText)
        {
            foreach(string orderDocument in bookingModel.MainOrderDocuments)
            {
                Assert.IsTrue(emailText.IndexOf(orderDocument) > 0);
            }

            foreach (string orderDocument in bookingModel.ReserveOrderDocuments)
            {
                Assert.IsTrue(emailText.IndexOf(orderDocument) > 0);
            }
        }
    }
}

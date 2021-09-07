using Amazon.SimpleEmail;
using book_a_reading_room_visit.api.Email;
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
        private const string HOME_URL = "HomeURL";
        private const string DOCUMENT_READING_ROOM = "Document reading room";
        private static readonly string RETURN_URL = $"{Environment.GetEnvironmentVariable(HOME_URL)}/return-to-booking";

        //private IAmazonSimpleEmailService _awsSes;
        private IEmailSender _awsSes;
        private IConfiguration _config;

        public EmailUnitTest()
        {
            _awsSes = Substitute.For<IEmailSender>();
            _config = Substitute.For<IConfiguration>();
        }

        [TestMethod]
        public async Task Email_GetText_BookingConfirmation()
        {
            BookingModel bookingModel = CreateBooking(BookingStatuses.Created, true);

            var emailService = new EmailService(_awsSes, _config);
            string emailText = emailService.GetTextBody(EmailType.BookingConfirmation, bookingModel);
            string visitType = GetVisitType(bookingModel);

            await CheckCoreFields(bookingModel, emailText);
            Assert.IsTrue(emailText.IndexOf(bookingModel.Phone) != -1);
            Assert.IsTrue(emailText.IndexOf(visitType) != -1);
            Assert.IsTrue(emailText.IndexOf(DOCUMENT_READING_ROOM) != -1);

            if (!String.IsNullOrWhiteSpace(bookingModel.AdditionalRequirements))
            {
                Assert.IsTrue(emailText.IndexOf(bookingModel.AdditionalRequirements) != -1);
            }

            await CheckDocumentOrders(bookingModel, emailText, true);

            Assert.IsTrue(!String.IsNullOrEmpty(Environment.GetEnvironmentVariable(HOME_URL)));
            Assert.IsTrue(emailText.IndexOf(RETURN_URL) != -1);
        }

        [TestMethod]
        public async Task Email_GetText_DSDBookingConfirmation()
        {
            BookingModel bookingModel = CreateBooking(BookingStatuses.Created, true);
            var emailService = new EmailService(_awsSes, _config);
            string emailText = emailService.GetTextBody(EmailType.DSDBookingConfirmation, bookingModel);

            await CheckCoreFields(bookingModel, emailText);

            Assert.IsTrue(emailText.IndexOf(bookingModel.Phone) != -1);

            await CheckDocumentOrders(bookingModel, emailText, false);
        }

        [TestMethod]
        public async Task Email_GetText_ReservationConfirmation()
        {
            BookingModel bookingModel = CreateBooking(BookingStatuses.Submitted, true);
            var emailService = new EmailService(_awsSes, _config);
            string emailText = emailService.GetTextBody(EmailType.ReservationConfirmation, bookingModel);
            string visitType = GetVisitType(bookingModel);

            await CheckCoreFields(bookingModel, emailText);
            Assert.IsTrue(emailText.IndexOf(visitType) != -1);
            Assert.IsTrue(emailText.IndexOf(DOCUMENT_READING_ROOM) != -1);

            Assert.IsTrue(!String.IsNullOrEmpty(Environment.GetEnvironmentVariable(HOME_URL)));
            Assert.IsTrue(emailText.IndexOf(RETURN_URL) != -1);
        }

        [TestMethod]
        public async Task Email_GetText_AutomaticCancellation()
        {
            BookingModel bookingModel = CreateBooking(BookingStatuses.Cancelled, true);
            var emailService = new EmailService(_awsSes, _config);
            string emailText = emailService.GetTextBody(EmailType.AutomaticCancellation, bookingModel);

            await CheckCancellation(bookingModel, emailText);
        }

        [TestMethod]
        public async Task Email_GetText_BookingCancellation()
        {
            BookingModel bookingModel = CreateBooking(BookingStatuses.Cancelled, true);
            var emailService = new EmailService(_awsSes, _config);
            string emailText = emailService.GetTextBody(EmailType.BookingCancellation, bookingModel);

            await CheckCancellation(bookingModel, emailText);
        }

        [TestMethod]
        public async Task Email_GetText_ValidOrderReminder()
        {
            BookingModel bookingModel = CreateBooking(BookingStatuses.Cancelled, true);
            var emailService = new EmailService(_awsSes, _config);
            string emailText = emailService.GetTextBody(EmailType.ValidOrderReminder, bookingModel);

            await CheckReminder(bookingModel, emailText);
            await CheckDocumentOrders(bookingModel, emailText, true);
        }

        [TestMethod]
        public async Task Email_GetText_InvalidOrderReminder()
        {
            BookingModel bookingModel = CreateBooking(BookingStatuses.Cancelled, true);
            var emailService = new EmailService(_awsSes, _config);
            string emailText = emailService.GetTextBody(EmailType.InvalidOrderReminder, bookingModel);

            await CheckReminder(bookingModel, emailText);
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
                ReaderTicket = 9497920,
                SeatNumber = "14H",
                SeatType = SeatTypes.StdRRSeat
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


        private async static Task CheckCoreFields(BookingModel bookingModel, string emailText)
        {
            Assert.IsTrue(emailText.IndexOf(bookingModel.BookingReference) != -1);
            Assert.IsTrue(emailText.IndexOf($"{bookingModel.FirstName} {bookingModel.LastName }") != -1);
            Assert.IsTrue(emailText.IndexOf(Convert.ToString(bookingModel.ReaderTicket.Value)) != -1);
            Assert.IsTrue(emailText.IndexOf(bookingModel.VisitStartDate.ToShortDateString()) > -1);
        }

        private async static Task CheckDocumentOrders(BookingModel bookingModel, string emailText, bool checkDescriptions)
        {
            foreach(string orderDocument in bookingModel.MainOrderDocuments)
            {
                (string Reference, string Description) orderDoc = GetOrderDocumentReferenceAndDescription(orderDocument);
                Assert.IsTrue(emailText.IndexOf(orderDoc.Reference) > 0);
                if (checkDescriptions)
                {
                    Assert.IsTrue(emailText.IndexOf(orderDoc.Description) > 1); 
                }
            }

            foreach (string orderDocument in bookingModel.ReserveOrderDocuments)
            {
                (string Reference, string Description) orderDoc = GetOrderDocumentReferenceAndDescription(orderDocument);
                Assert.IsTrue(emailText.IndexOf(orderDoc.Reference) > 0);
                if (checkDescriptions)
                {
                    Assert.IsTrue(emailText.IndexOf(orderDoc.Description) > 1);
                }
            }
        }

        private async static Task CheckCancellation(BookingModel bookingModel, string emailText)
        {
            await CheckCoreFields(bookingModel, emailText);
            string visitType = GetVisitType(bookingModel);
            Assert.IsTrue(emailText.IndexOf(visitType) != -1);
            //Assert.IsTrue(emailText.IndexOf("Standard Reading Room") != -1);

            string homeUrl = Environment.GetEnvironmentVariable(HOME_URL);

            Assert.IsTrue(!String.IsNullOrEmpty(homeUrl));
            Assert.IsTrue(emailText.IndexOf(homeUrl) != -1);
        }

        private async static Task CheckOrderReminder(BookingModel bookingModel, string emailText)
        {
            await CheckCoreFields(bookingModel, emailText);
            string visitType = GetVisitType(bookingModel);
            Assert.IsTrue(emailText.IndexOf(visitType) != -1);
            Assert.IsTrue(emailText.IndexOf("Standard Reading Room") != -1);

            Assert.IsTrue(!String.IsNullOrEmpty(Environment.GetEnvironmentVariable(HOME_URL)));
            Assert.IsTrue(emailText.IndexOf(Environment.GetEnvironmentVariable(HOME_URL)) != -1);
        }

        private async static Task CheckReminder(BookingModel bookingModel, string emailText)
        {
            await CheckCoreFields(bookingModel, emailText);
            string visitType = GetVisitType(bookingModel);
            Assert.IsTrue(emailText.IndexOf(visitType) != -1);
            Assert.IsTrue(emailText.IndexOf(DOCUMENT_READING_ROOM) != -1);

            Assert.IsTrue(!String.IsNullOrEmpty(Environment.GetEnvironmentVariable(HOME_URL)));
            Assert.IsTrue(emailText.IndexOf(RETURN_URL) != -1);
        }

        private static string GetVisitType(BookingModel bookingModel)
        {
            return bookingModel.BookingType == BookingTypes.StandardOrderVisit ? "Standard visit" : "Bulk order visit";
        }

        private static (string Reference, string Description) GetOrderDocumentReferenceAndDescription(string orderDocument)
        {
            string[] orderDocumentComponents = orderDocument.Substring(orderDocument.IndexOf("Reference:") + 11).Split(", Description").Select(s => s.Trim(new char[] { ' ', ':', '.' })).ToArray();
            (string Reference, string Description) t = (orderDocumentComponents[0], orderDocumentComponents[1]);
            return t;
        }
    }
}

using book_a_reading_room_visit.domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.test
{
    [TestClass]
    public class BookingIntegrationTest
    {
        private readonly CustomWebApplicationFactory<book_a_reading_room_visit.api.Startup> _factory;

        public BookingIntegrationTest()
        {
            _factory = new CustomWebApplicationFactory<book_a_reading_room_visit.api.Startup>();
        }

        [TestMethod]
        public async Task SearchBookings_Using_Default_InMemoryDB_All_Params()
        {
            string apiEndpoint = "/book-a-visit-api/Booking/search?Date=2021-03-15&BookingReference=B123456&ReadersTicket=9497920";
            await SearchBookings_Using_Default_InMemoryDB(apiEndpoint);
        }

        [TestMethod]
        public async Task SearchBookings_Using_Default_InMemoryDB_Date_Only()
        {
            string apiEndpoint = "/book-a-visit-api/Booking/search?Date=2021-03-15";
            await SearchBookings_Using_Default_InMemoryDB(apiEndpoint);
        }

        [TestMethod]
        public async Task SearchBookings_Using_Default_InMemoryDB_Booking_Reference_Only()
        {
            string apiEndpoint = "/book-a-visit-api/Booking/search?BookingReference=B123456";
            await SearchBookings_Using_Default_InMemoryDB(apiEndpoint);
        }

        [TestMethod]
        public async Task SearchBookings_Using_Default_InMemoryDB_Reader_Ticket_Only()
        {
            string apiEndpoint = "/book-a-visit-api/Booking/search?ReadersTicket=9497920";
            await SearchBookings_Using_Default_InMemoryDB(apiEndpoint);
        }

        [TestMethod]
        public async Task SearchBookings_Using_Default_InMemoryDB_No_Params()
        {
            string apiEndpoint = "/book-a-visit-api/Booking/search";
            await SearchBookings_Using_Default_InMemoryDB(apiEndpoint);
        }

        private async Task SearchBookings_Using_Default_InMemoryDB(string apiEndpoint)
        {
            // Arrange
            var client = _factory.CreateClient();
            // Act
            var response = await client.GetAsync(apiEndpoint);
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var responseString = await response.Content.ReadAsStringAsync();
            List<Booking> responseObject = JsonConvert.DeserializeObject<List<Booking>>(responseString);
            // Assert
            Assert.AreEqual(1, responseObject.Count);
            Assert.AreEqual("B123456", responseObject[0].BookingReference);
            Assert.AreEqual(9497920, responseObject[0].ReaderTicket);
            Assert.AreEqual(DateTime.Parse("2021-03-15"), responseObject[0].VisitStartDate);
        }
    }
}

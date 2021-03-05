using book_a_reading_room_visit.api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.test
{
    [TestClass]
    public class APIIntegrationTest
    {
        private readonly CustomWebApplicationFactory<book_a_reading_room_visit.api.Startup> _factory;

        public APIIntegrationTest()
        {
            _factory = new CustomWebApplicationFactory<book_a_reading_room_visit.api.Startup>();
        }

        [TestMethod]
        public async Task GetAvailabilitySummary_Using_Default_InMemoryDB()
        {
            // Arrange
            var client = _factory.CreateClient();
            // Act
            var response = await client.GetAsync("/book-a-visit-api/availability/summary");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<AvailabilitySummaryModel>(responseString);
            // Assert
            Assert.AreEqual(1980, responseObject.StandardBookingAvailable);
            Assert.AreEqual(32, responseObject.BulkBookingsAvailable);
        }
    }
}

using book_a_reading_room_visit.api.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.test
{
    [TestClass]
    public class EmailHeaderProviderUnitTest
    {
        private readonly IConfiguration _configuration;
        public EmailHeaderProviderUnitTest()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _configuration = config;
        }

        [TestMethod]
        public void CheckAdditionalHeaders()
        {
            var emailHeaderProvider = new EmailHeaderProvider(_configuration);
            IReadOnlyCollection<EmailHeader> dsdHeaders = emailHeaderProvider.DsdConfirmationHeaders;
            Assert.IsTrue(dsdHeaders.Count == 2);
            Assert.IsTrue(dsdHeaders.First().Name == "X-SES-CONFIGURATION-SET");
            Assert.IsTrue(dsdHeaders.First().Value == "KBSEmailsToDSD");
            Assert.IsTrue(dsdHeaders.First().ToString() == "X-SES-CONFIGURATION-SET: KBSEmailsToDSD");

            //CustomerConfirmation is  misspelt in appsettings.json as CustomerConfirmations, so we shoudl get back an empty array
            IReadOnlyCollection<EmailHeader> customerHeaders = emailHeaderProvider.CustomerConfirmationHeaders;
            Assert.IsTrue(customerHeaders.Count == 2);
        }
    }
}

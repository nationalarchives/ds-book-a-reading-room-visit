using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace book_a_reading_room_visit.api.Email
{
    
    internal class EmailHeaderProvider
    {
        private readonly IConfiguration _configuration;

        private EmailHeader[] _dsdConfirmationHeaders;
        private EmailHeader[] _customerConfirmationHeaders;

        public EmailHeaderProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IReadOnlyCollection<EmailHeader> DsdConfirmationHeaders
        {
            get 
            { 
                if(_dsdConfirmationHeaders == null)
                {
                    IConfigurationSection additionalHeadersEmailSection = _configuration.GetSection("EmailSettings").GetSection("AdditionalHeaders").GetChildren().FirstOrDefault(c => c.Key == "DsdConfirmation");
                    _dsdConfirmationHeaders = additionalHeadersEmailSection == null ? new EmailHeader[0] :
                        additionalHeadersEmailSection.GetChildren().Select(c => new EmailHeader(c.Key, c.Value)).ToArray();
                }
                return new ReadOnlyCollection<EmailHeader>(_dsdConfirmationHeaders); 
            }
        }

        public IReadOnlyCollection<EmailHeader> CustomerConfirmationHeaders
        {
            get 
            {
                if (_customerConfirmationHeaders == null)
                {
                    IConfigurationSection additionalHeadersEmailSection = _configuration.GetSection("EmailSettings").GetSection("AdditionalHeaders").GetChildren().FirstOrDefault(c => c.Key == "CustomerConfirmation");
                    _customerConfirmationHeaders = additionalHeadersEmailSection == null ? new EmailHeader[0] :
                        additionalHeadersEmailSection.GetChildren().Select(c => new EmailHeader(c.Key, c.Value)).ToArray();
                }
                return new ReadOnlyCollection<EmailHeader>(_customerConfirmationHeaders); 
            }
        }
    }
}
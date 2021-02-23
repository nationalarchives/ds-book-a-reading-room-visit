using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.web.Service
{
    public class BookAVisitAPI
    {
        public HttpClient _client { get; }

        public BookAVisitAPI(HttpClient client, IConfiguration configuration)
        {
            client.BaseAddress = new Uri(configuration.GetValue<string>("book-a-visit-api"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _client = client;
        }
    }
}

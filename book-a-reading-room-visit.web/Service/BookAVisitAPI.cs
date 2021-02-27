using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace book_a_reading_room_visit.web.Service
{
    public class BookAVisitAPI
    {
        public HttpClient _client { get; }

        public BookAVisitAPI(HttpClient client)
        {
            client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("KBS_WebApi_URL"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _client = client;
        }
    }
}

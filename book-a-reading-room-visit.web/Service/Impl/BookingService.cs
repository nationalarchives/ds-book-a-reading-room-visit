using System.Threading.Tasks;
using System.Net.Http.Json;
using book_a_reading_room_visit.web.Models;
using System.Net.Http;
using book_a_reading_room_visit.domain;
using System.Collections.Generic;

namespace book_a_reading_room_visit.web.Service
{
    public class BookingService : IBookingService
    {
        public HttpClient _client { get; }
        public BookingService(HttpClient client)
        {
            _client = client;
        }
        public async Task<BookingResponseModel> CreateBookingAsync(BookingViewModel bookingViewModel)
        {
            var response = await _client.PostAsJsonAsync("booking/create", bookingViewModel);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<BookingResponseModel>();
            return result;
        }
    }
}

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

        public async Task<BookingResponseModel> ReserveSpaceAsync(BookingViewModel bookingViewModel)
        {
            var response = await _client.PostAsJsonAsync("booking/confirm", bookingViewModel);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<BookingResponseModel>();
            return result;
        }

        public async Task<BookingViewModel> GetBookingAsync(int readerTicket, string bookingReference)
        {
            var response = await _client.GetAsync($"booking/{readerTicket}/{bookingReference}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<BookingViewModel>();
            return result;
        }

        public async Task DeleteBookingAsync(string bookingReference)
        {
            var response = await _client.DeleteAsync($"booking/{bookingReference}");
            response.EnsureSuccessStatusCode();
        }

        public async Task CancelBookingAsync(CancelViewModel cancelViewModel)
        {
            var response = await _client.PostAsJsonAsync("booking/cancel", cancelViewModel);
            response.EnsureSuccessStatusCode();
        }
    }
}

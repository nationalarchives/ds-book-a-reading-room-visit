using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net;
using System.Net.Http;
using book_a_reading_room_visit.model;

namespace book_a_reading_room_visit.web.Service
{
    public class BookingService : IBookingService
    {
        public HttpClient _client { get; }
        public BookingService(HttpClient client)
        {
            _client = client;
        }
        public async Task<BookingResponseModel> CreateBookingAsync(BookingModel bookingModel)
        {
            var response = await _client.PostAsJsonAsync("booking/create", bookingModel);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<BookingResponseModel>();
            return result;
        }

        public async Task<BookingResponseModel> ReserveSpaceAsync(BookingModel bookingModel)
        {
            var response = await _client.PostAsJsonAsync("booking/confirm", bookingModel);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<BookingResponseModel>();
            return result;
        }

        public async Task<BookingResponseModel> UpsertDocumentAsync(BookingModel bookingModel)
        {
            var response = await _client.PostAsJsonAsync("booking/upsert-document", bookingModel);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<BookingResponseModel>();
            return result;
        }

        public async Task<BookingModel> GetBookingAsync(string bookingReference)
        {
            var response = await _client.GetAsync($"booking/{bookingReference}");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<BookingModel>();
            return result;
        }

        public async Task<BookingModel> GetBookingAsync(int readerTicket, string bookingReference)
        {
            var response = await _client.GetAsync($"booking/{readerTicket}/{bookingReference}");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<BookingModel>();
            return result;
        }

        public async Task DeleteBookingAsync(string bookingReference)
        {
            var response = await _client.DeleteAsync($"booking/{bookingReference}");
            response.EnsureSuccessStatusCode();
        }

        public async Task CancelBookingAsync(BookingCancellationModel bookingCancellationModel)
        {
            var response = await _client.PostAsJsonAsync("booking/cancel", bookingCancellationModel);
            response.EnsureSuccessStatusCode();
        }
    }
}

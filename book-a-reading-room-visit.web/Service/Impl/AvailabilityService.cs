using book_a_reading_room_visit.model;
using book_a_reading_room_visit.web.Models;

namespace book_a_reading_room_visit.web.Service
{
    public class AvailabilityService : IAvailabilityService
    {
        public HttpClient _client { get; }
        public AvailabilityService(HttpClient client)
        {
            _client = client;
        }

        public async Task<AvailabilitySummaryModel> GetAvailabilitySummaryAsync()
        {
            var response = await _client.GetAsync("availability/summary");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<AvailabilitySummaryModel>();
            return result;
        }

        public async Task<List<AvailableSeat>> GetAvailabilityAsync(SeatTypes seatType)
        {
            var response = await _client.GetAsync($"availability/seats-count-by-seattype?seatType={seatType}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<List<AvailableSeat>>();
            return result;
        }
    }
}

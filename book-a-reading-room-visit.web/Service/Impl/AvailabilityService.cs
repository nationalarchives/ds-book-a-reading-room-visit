using System.Threading.Tasks;
using System.Net.Http.Json;
using book_a_reading_room_visit.web.Models;
using System.Net.Http;
using book_a_reading_room_visit.domain;
using System.Collections.Generic;

namespace book_a_reading_room_visit.web.Service
{
    public class AvailabilityService : IAvailabilityService
    {
        public HttpClient _client { get; }
        public AvailabilityService(HttpClient client)
        {
            _client = client;
        }

        public async Task<AvailabilitySummaryViewModel> GetAvailabilitySummaryAsync()
        {
            var response = await _client.GetFromJsonAsync<AvailabilitySummaryViewModel>("availability/summary");
            return response;
        }

        public async Task<List<AvailableSeat>> GetAvailabilityAsync(SeatTypes seatType)
        {
            var response = await _client.GetFromJsonAsync<List<AvailableSeat>>($"availability?seatType={seatType}");

            return response;
        }
    }
}

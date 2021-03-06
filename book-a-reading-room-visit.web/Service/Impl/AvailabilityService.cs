﻿using System.Threading.Tasks;
using System.Net.Http.Json;
using book_a_reading_room_visit.web.Models;
using System.Net.Http;
using System.Collections.Generic;
using book_a_reading_room_visit.model;

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

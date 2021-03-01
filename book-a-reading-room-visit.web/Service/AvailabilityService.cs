using book_a_reading_room_visit.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using book_a_reading_room_visit.web.Models;

namespace book_a_reading_room_visit.web.Service
{
    public class AvailabilityService
    {
        private readonly BookAVisitAPI _bookAVisitAPI;
        public AvailabilityService(BookAVisitAPI bookAVisitAPI)
        {
            _bookAVisitAPI = bookAVisitAPI;
        }

        public async Task<AvailabilitySummaryViewModel> GetAvailabilitySummaryAsync()
        {
            var response = await _bookAVisitAPI._client.GetFromJsonAsync<AvailabilitySummaryViewModel>("/book-a-visit-api/availability/summary");

            return response;
        }
    }
}

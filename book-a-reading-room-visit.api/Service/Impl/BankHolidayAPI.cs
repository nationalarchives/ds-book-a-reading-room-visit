using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using book_a_reading_room_visit.model;

namespace book_a_reading_room_visit.api.Service
{
    public class BankHolidayAPI : IBankHolidayAPI
    {
        public HttpClient _client { get; }
        private IMemoryCache _cache;

        public BankHolidayAPI(HttpClient client, IMemoryCache memoryCache)
        {
            _client = client;
            _cache = memoryCache;
        }

        public async Task<List<DateTime>> GetBankHolidaysAsync()
        {
            if (_cache.TryGetValue("BankHolidays", out List<DateTime> bankHolidays))
            {
                return bankHolidays;
            }
            var response = await _client.GetAsync("getbankholidays");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<BankHoliday[]>();

            bankHolidays = result?.Select(h => h.Date).ToList() ?? new List<DateTime>();

            _cache.Set("BankHolidays", bankHolidays, DateTime.Today.AddDays(1));
            return bankHolidays;
        }
    }
}

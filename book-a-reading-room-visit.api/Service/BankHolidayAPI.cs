using book_a_reading_room_visit.api.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

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
            List<DateTime> bankHolidays = null;
            if (!_cache.TryGetValue<List<DateTime>>("BankHolidays", out bankHolidays))
            {
                var result = await _client.GetFromJsonAsync<BankHoliday[]>("getbankholidays");
                bankHolidays = result.Select(h => h.Date).ToList();

                _cache.Set<List<DateTime>>("BankHolidays", bankHolidays, DateTime.Now.AddDays(1));
            }
            return bankHolidays;
        }
    }
}

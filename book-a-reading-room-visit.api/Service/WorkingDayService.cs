using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Service
{
    public class WorkingDayService
    {
        private readonly IConfiguration _configuration;
        public WorkingDayService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<DateTime>> GetAvailableDatesAsync()
        {
            var availableDates = new List<DateTime>();
            var availabilityFrom = int.Parse(_configuration.GetSection("BookingTimeLine:AvailabilityFrom").Value);
            var availabilityDatesToShow = int.Parse(_configuration.GetSection("BookingTimeLine:AvailabilityDatesToShow").Value);
            // Have to consider bank holidays
            var startDate = DateTime.Today.AddDays(availabilityFrom);

            for (int i = 0; i < availabilityDatesToShow; i++)
            {
                availableDates.Add(startDate.AddDays(i));
            }
            return availableDates;
        }
    }
}

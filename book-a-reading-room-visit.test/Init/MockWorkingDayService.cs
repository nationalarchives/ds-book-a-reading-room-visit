using book_a_reading_room_visit.api.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.test
{
    public class MockWorkingDayService : IWorkingDayService
    {
        public async Task<List<DateTime>> GetBulkOrderAvailableDatesAsync()
        {
            var dates = new List<DateTime>();
            var startDate = DateTime.Today.AddDays(5);

            for (int i = 0; i < 8; i++)
            {
                dates.Add(startDate.AddDays(i));
            }
            return dates;
        }

        public Task<DateTime> GetCompleteByDateAsync(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public Task<DateTime> GetNextBulkOrderOpeningDayAsync(DateTime dateTime, int daysToAdd)
        {
            throw new NotImplementedException();
        }

        public Task<DateTime> GetNextStandardOrderOpeningDayAsync(DateTime dateTime, int daysToAdd)
        {
            throw new NotImplementedException();
        }

        public Task<DateTime> GetNextWorkingDayAsync(DateTime dateTime, int daysToAdd)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DateTime>> GetStandardOrderAvailableDatesAsync()
        {
            var dates = new List<DateTime>();
            var startDate = DateTime.Today.AddDays(5);

            for (int i = 0; i < 20; i++)
            {
                dates.Add(startDate.AddDays(i));
            }
            return dates;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Service
{
    public interface IWorkingDayService
    {
        Task<List<DateTime>> GetStandardOrderAvailableDatesAsync();
        Task<List<DateTime>> GetBulkOrderAvailableDatesAsync();
        Task<DateTime> GetNextWorkingDayAsync(DateTime dateTime, int daysToAdd);
        Task<DateTime> GetNextStandardOrderOpeningDayAsync(DateTime dateTime, int daysToAdd);
        Task<DateTime> GetNextBulkOrderOpeningDayAsync(DateTime dateTime, int daysToAdd);
    }
}

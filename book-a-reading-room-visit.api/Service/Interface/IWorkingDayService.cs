using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Service
{
    public interface IWorkingDayService
    {
        Task<List<DateTime>> GetOneDayOrderAvailableDatesAsync();
        Task<List<DateTime>> GetTwoDayOrderAvailableDatesAsync();
        Task<DateTime> GetNextWorkingDayAsync(DateTime dateTime, int daysToAdd);
        Task<DateTime> GetNextOneDayOrderOpeningDayAsync(DateTime dateTime, int daysToAdd);
        Task<DateTime> GetNextTwoDayOrderOpeningDayAsync(DateTime dateTime, int daysToAdd);
        Task<DateTime> GetCompleteByDateAsync(DateTime dateTime);
    }
}

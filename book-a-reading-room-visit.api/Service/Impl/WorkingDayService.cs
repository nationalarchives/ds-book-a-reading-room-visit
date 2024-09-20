using book_a_reading_room_visit.data;
using book_a_reading_room_visit.domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace book_a_reading_room_visit.api.Service
{
    public class WorkingDayService : IWorkingDayService
    {
        private IMemoryCache _cache;
        private readonly IConfiguration _configuration;
        private readonly BookingContext _context;
        private readonly DayOfWeek[] WorkingWeekDays = new DayOfWeek[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };
        private readonly DayOfWeek[] OneDayOrderOpeningWeekDays = new DayOfWeek[] { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday };
        private readonly DayOfWeek[] TwoDayOrderOpeningWeekDays = new DayOfWeek[] { DayOfWeek.Tuesday, DayOfWeek.Thursday, };

        public WorkingDayService(IConfiguration configuration, BookingContext context, IMemoryCache memoryCache)
        {
            _configuration = configuration;
            _context = context; 
            _cache = memoryCache;
        }
        public async Task<List<DateTime>> GetOneDayOrderAvailableDatesAsync()
        {
            if (_cache.TryGetValue("OneDayOrderAvailableDates", out List<DateTime> availableDates))
            {
                return availableDates;
            }

            availableDates = new List<DateTime>();
            var availabilityFrom = int.Parse(_configuration.GetSection("BookingTimeLine:DocumentsPreparationDays").Value);
            var availabilityDatesToShow = int.Parse(_configuration.GetSection("BookingTimeLine:OneDayAvailabilityDatesToShow").Value);

            var startDate = await GetNextWorkingDayAsync(DateTime.Today, availabilityFrom);

            for (int i = 0; i < availabilityDatesToShow; i++)
            {
                startDate = await GetNextOneDayOrderOpeningDayAsync(startDate, 1);
                availableDates.Add(startDate);
            }
            _cache.Set("OneDayOrderAvailableDates", availableDates, DateTime.Today.AddDays(1));
            return availableDates;
        }

        public async Task<List<DateTime>> GetTwoDayOrderAvailableDatesAsync()
        {
            if (_cache.TryGetValue("TwoDayOrderAvailableDates", out List<DateTime> availableDates))
            {
                return availableDates;
            }

            availableDates = new List<DateTime>();
            var availabilityFrom = int.Parse(_configuration.GetSection("BookingTimeLine:DocumentsPreparationDays").Value);
            var availabilityDatesToShow = int.Parse(_configuration.GetSection("BookingTimeLine:TwoDayAvailabilityDatesToShow").Value);

            var startDate = await GetNextWorkingDayAsync(DateTime.Today, availabilityFrom);

            for (int i = 0; i < availabilityDatesToShow; i++)
            {
                startDate = await GetNextTwoDayOrderOpeningDayAsync(startDate, 1);
                availableDates.Add(startDate);
            }
            _cache.Set("TwoDayOrderAvailableDates", availableDates, DateTime.Today.AddDays(1));
            return availableDates;
        }


        public async Task<DateTime> GetNextWorkingDayAsync(DateTime dateTime, int daysToAdd)
        {
            var holidays = await (from holiday in _context.Set<TNAHoliday>()
                                    select holiday.date_closed).ToListAsync();

            var dateToReturn = dateTime;
            while (daysToAdd > 0)
            {
                dateToReturn = dateToReturn.AddDays(1);
                if (WorkingWeekDays.Contains(dateToReturn.DayOfWeek) && !holidays.Contains(dateToReturn))
                    daysToAdd--;
            }
            return dateToReturn;
        }

        public async Task<DateTime> GetNextOneDayOrderOpeningDayAsync(DateTime dateTime, int daysToAdd)
        {
            var holidays = await (from holiday in _context.Set<TNAHoliday>()
                                  select holiday.date_closed).ToListAsync();
            var dateToReturn = dateTime;
            while (daysToAdd > 0)
            {
                dateToReturn = dateToReturn.AddDays(1);
                if (OneDayOrderOpeningWeekDays.Contains(dateToReturn.DayOfWeek) && !holidays.Contains(dateToReturn))
                    daysToAdd--;
            }
            return dateToReturn;
        }

        public async Task<DateTime> GetNextTwoDayOrderOpeningDayAsync(DateTime dateTime, int daysToAdd)
        {
            var holidays = await (from holiday in _context.Set<TNAHoliday>()
                                  select holiday.date_closed).ToListAsync();
            var dateToReturn = dateTime;
            while (daysToAdd > 0)
            {
                dateToReturn = dateToReturn.AddDays(1);
                if (TwoDayOrderOpeningWeekDays.Contains(dateToReturn.DayOfWeek) && !holidays.Contains(dateToReturn))
                    daysToAdd--;
            }
            return dateToReturn;
        }

        public async Task<DateTime> GetCompleteByDateAsync(DateTime dateTime)
        {
            int daysToDeduct = int.Parse(_configuration.GetSection("BookingTimeLine:DocumentsPreparationDays").Value);
            var holidays = await (from holiday in _context.Set<TNAHoliday>()
                                  select holiday.date_closed).ToListAsync();
            var dateToReturn = dateTime;
            while (daysToDeduct > 0)
            {
                dateToReturn = dateToReturn.AddDays(-1);
                if (WorkingWeekDays.Contains(dateToReturn.DayOfWeek) && !holidays.Contains(dateToReturn))
                    daysToDeduct--;
            }
            return dateToReturn.AddMinutes(-1);
        }
    }
}

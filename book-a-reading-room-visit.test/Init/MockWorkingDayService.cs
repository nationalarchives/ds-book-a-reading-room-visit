using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.test
{
    public class MockWorkingDayService
    {
        public async Task<List<DateTime>> GetAvailableDatesAsync()
        {
            var dates = new List<DateTime>();
            var startDate = DateTime.Today.AddDays(5);

            for (int i = 0;i < 20;i++)
            {
                dates.Add(startDate.AddDays(i));
            }
            return dates;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Service
{
    public interface IBankHolidayAPI
    {
        Task<List<DateTime>> GetBankHolidaysAsync();
    }
}

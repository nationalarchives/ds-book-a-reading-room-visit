using book_a_reading_room_visit.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Helper
{
    internal static class BookingTypeHelper
    {
        public static int NumberOfDays(this BookingTypes bookingTypeEnum)
        {
            int numberOfDays;

            switch(bookingTypeEnum)
            {
                case BookingTypes.StandardOrderVisit:
                case BookingTypes.ComputerUseVisit:
                    numberOfDays = 1;
                    break;
                case BookingTypes.BulkOrderVisit:
                    numberOfDays = 2;
                    break;
                default:
                    throw new ArgumentException("Invalid booking type.");

            }

            return numberOfDays;
        }
    }
}

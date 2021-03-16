using book_a_reading_room_visit.domain;

namespace book_a_reading_room_visit.api.Helper
{
    public static class SeatTypeHelper
    {
        private static SeatTypes[] _oneDayVisitSeatTypes = new SeatTypes[] {SeatTypes.StdRRSeat, SeatTypes.MandLRR };
        private static SeatTypes[] _twoDayVisitSeatTypes = new SeatTypes[] { SeatTypes.BulkOrderSeat };

        public static SeatTypes[] OneDayVisit
        {
            get => _oneDayVisitSeatTypes;
        }

        public static SeatTypes[] TwoDayVisit
        {
            get => _twoDayVisitSeatTypes;
        }
    }
}

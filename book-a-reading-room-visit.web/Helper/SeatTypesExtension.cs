using book_a_reading_room_visit.domain;

namespace book_a_reading_room_visit.web.Helper
{
    public static class SeatTypesExtension
    {
        public static string ToStringURL(this SeatTypes seatTypes)
        {
            switch (seatTypes)
            {
                case SeatTypes.StdRRSeat:
                    return "standard-reading-room";
                case SeatTypes.MandLRR:
                    return "map-and-large-document-reading-room";
                default:
                    return string.Empty;
            }
        }

        public static SeatTypes ToSeatType(this string seatTypes)
        {
            switch (seatTypes?.ToLower())
            {
                case "map-and-large-document-reading-room":
                    return SeatTypes.MandLRR;
                case "standard-reading-room":
                    return SeatTypes.StdRRSeat;
                default:
                    return SeatTypes.BulkOrderSeat;
            }
        }
    }
}

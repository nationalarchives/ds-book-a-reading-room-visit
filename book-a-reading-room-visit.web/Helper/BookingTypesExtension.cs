using book_a_reading_room_visit.domain;

namespace book_a_reading_room_visit.web.Helper
{
    public static class BookingTypesExtension
    {
        public static string ToStringURL(this BookingTypes bookingTypes)
        {
            switch (bookingTypes)
            {
                case BookingTypes.StandardOrderVisit:
                    return "standard-order-visit";
                case BookingTypes.BulkOrderVisit:
                    return "bulk-order-visit";
                default:
                    return string.Empty;
            }
        }

        public static BookingTypes ToBookingType(this string bookingTypes)
        {
            switch (bookingTypes.ToLower())
            {
                case "bulk-order-visit":
                    return BookingTypes.BulkOrderVisit;
                default:
                    return BookingTypes.StandardOrderVisit;
            }
        }
    }
}

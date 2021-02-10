namespace book_a_reading_room_visit.web.Models
{
    public enum OrderType
    {
        None = -1,
        StandardOrderVisit = 0,
        BulkOrderVisit = 1
    }

    public static class OrderTypeExtensions
    {
        public static string ToStringURL(this OrderType orderType)
        {
            switch (orderType)
            {
                case OrderType.StandardOrderVisit:
                    return "standard-order-visit";
                case OrderType.BulkOrderVisit:
                    return "bulk-order-visit";
                default:
                    return string.Empty;
            }
        }

        public static OrderType ToOrderType(this string orderType)
        {
            switch (orderType.ToLower())
            {
                case "standard-order-visit":
                    return OrderType.StandardOrderVisit;
                case "bulk-order-visit":
                    return OrderType.BulkOrderVisit;
                default:
                    return OrderType.None;
            }
        }
    }
}

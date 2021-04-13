namespace book_a_reading_room_visit.web.Helper
{
    public static class ErrorCodeExtension
    {
        public static string ToMessage(this ErrorCode errorCode)
        {
            switch (errorCode)
            {
                case ErrorCode.seat_unavailable:
                    return "This date is no longer available. Choose another date.";
                case ErrorCode.reserved_time_expired:
                    return "Your booking has timed out. Choose a date of visit again.";
                default:
                    return string.Empty;
            }
        }
    }

    public enum ErrorCode
    {
        seat_unavailable = 1,
        reserved_time_expired = 2
    }
}

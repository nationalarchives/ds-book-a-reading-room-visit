namespace book_a_reading_room_visit.web.Helper
{
    public static class ErrorCodeExtension
    {
        public static string ToMessage(this ErrorCode errorCode)
        {
            switch (errorCode)
            {
                case ErrorCode.seat_unavailable:
                    return "The selected date is no longer available to book, please select another date";
                case ErrorCode.reserved_time_expired:
                    return "The reserved time elapsed, please select date again.";
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

namespace book_a_reading_room_visit.web.Helper
{
    public class Constants
    {
        public const string Valid_Ticket_Required = "Enter a valid reader’s ticket number or temporary reader’s ticket number.";
        public const string Valid_BookingReference_Required = "Enter a valid booking reference number.";
        public const string Valid_Ticket_And_BookingReference_Required = "This booking does not exist. Check your details and try again.";
        public const string BookingReference_Is_Cancelled = "You cannot view this document order as your visit was cancelled.";

        public const string Ticket_Exceed_Order_Limit = "You currently have 36 visits booked over a 12-week period. Choose a later date.";
        public const string Another_Booking_On_The_Same_date = "You have a visit booked on this date already. Choose a different date.";
        public const string Firstname_Required = "Enter a first name.";
        public const string Lastname_Required = "Enter a last name.";
        public const string Valid_Email_Required = "Enter a valid email address.";
        public const string Phone_Or_Email_Required = "Enter an email address or telephone number so we can contact you.";
        public const string Series_Required = "Enter a series number.";
        public const string Accept_Terms_Privacy_Required = "Accept our terms of use and privacy policy.";
        public const string Html_Tags_Not_Allowed = "You have entered an <HTML> tag. Only include numbers, letters A to Z, and special characters.";

        public const string Duplicate_Document_Reference = "You have already entered this catalogue reference.";
        public const string Document_Reference_Series_Not_Matched = "This document is from a different series to the one you have entered above. Check the catalogue series and try again.";
        public const string Document_Series_Cannot_Order = "This document series cannot be ordered. Check the catalogue for more information.";

        public const string CookieName = "cookies_policy";
        public const string Localhost = "localhost";
        public const string TNADomain = "nationalarchives.gov.uk";

        public const string Invalid_Document_Reference = "The document reference you have entered has an invalid format.  References must start with 1-4 letters, followed by a space, then 1-4 digits";
    }
}

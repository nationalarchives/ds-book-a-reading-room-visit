namespace book_a_reading_room_visit.model
{
    public enum EmailType
    {
        ReservationConfirmation = 1,
        ValidOrderReminder = 2,
        InvalidOrderReminder = 3,
        BookingConfirmation = 4,
        BookingCancellation = 5,
        AutomaticCancellation = 6,
        DSDBookingConfirmation = 7,
    }
}

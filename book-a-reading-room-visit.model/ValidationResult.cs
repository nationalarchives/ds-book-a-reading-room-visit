namespace book_a_reading_room_visit.model
{
    public enum ValidationResult
    {
        AllowToBook = 0,
        HaveAnotherVisitOnThisDate = 1,
        ExceededTheSetLimit = 2,
        Error = 3
    }
}

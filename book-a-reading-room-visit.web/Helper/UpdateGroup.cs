using book_a_reading_room_visit.web.Models;

namespace book_a_reading_room_visit.web.Helper
{
    public static class UpdateGroup
    {
        public static AvailabilityViewModel UpdateCategory(this AvailabilityViewModel model)
        {
            var month = "";
            var found = false;
            var index = 0;
            AvailableSeatGroup group = null;
            foreach (var record in model.AvailableBookings)
            {
                if (record.AvailableSeats > 0 && !found)
                {
                    found = true;
                    record.Id = "first-available";
                    model.FirstAvailableDate = record.Date.ToString("dddd dd MMMM yyyy");
                }
                else
                {
                    record.Id = $"row-{index}";
                }
                if (month != record.Date.ToString("MMMM yyyy"))
                {
                    month = record.Date.ToString("MMMM yyyy");
                    if (group != null)
                    {
                        model.AvailableSeatGroups.Add(group);
                    }
                    group = new AvailableSeatGroup
                    {
                        Month = month,
                        StartIndex = index
                    };
                }
                group.EndIndex = index;
                index++;
            }
            model.AvailableSeatGroups.Add(group);
            return model;
        }
    }
}

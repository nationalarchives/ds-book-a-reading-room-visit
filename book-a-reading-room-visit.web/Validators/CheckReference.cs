using System.ComponentModel.DataAnnotations;

namespace book_a_reading_room_visit.web.Validators
{
    public class CheckReference : RegularExpressionAttribute
    {
        public CheckReference() : base(@"^[a-zA-Z]{1,4} \d{1,4}.*")
        {

        }
    }
}

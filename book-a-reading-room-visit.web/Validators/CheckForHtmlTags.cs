using System.ComponentModel.DataAnnotations;

namespace book_a_reading_room_visit.web.Validators
{
    public class CheckForHtmlTags : RegularExpressionAttribute
    {
        public CheckForHtmlTags() : base(@"^(?!.*<[^>]+>).*")
        {

        }
    }
}

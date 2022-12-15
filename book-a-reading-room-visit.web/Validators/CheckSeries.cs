using System.ComponentModel.DataAnnotations;

namespace book_a_reading_room_visit.web.Validators
{
    public class CheckSeries : RegularExpressionAttribute
    {
        // Users must include a space in the series reference.  Since the bulk order
        // service doens;t recognise series references without a space, although it's
        // OK with unspaced piece and item refereneces.
        private static readonly string mainRegex = @"^[A-Z]{1,4} {1}[0-9]{1,4}(\/[0-9])?$";

        public CheckSeries() : base(mainRegex)
        {

        }

        public override bool IsValid(object value)
        {
            string input = (value as string)?.Trim();
            bool isValid = base.IsValid(input);
            return isValid;
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace book_a_reading_room_visit.web.Validators
{
    public class CheckReference : RegularExpressionAttribute
    {
        private static readonly Regex seriesSpecific1 = new Regex(@"^(CP ?2[456]|IR ?(12[14-9]|13[0-5])|PRO ?(3[01]|41|66))");
        private static readonly Regex seriesSpecific2 = new Regex(@"^(CP ?2[456]\/|IR ?(12[14-9]|13[0-5])\/|PRO ?(3[01]|41|66)\/)[0-9]");
        private static readonly string mainRegex = @"^[A-Z]{1,4} ?[0-9]{1,4}.*";

        public CheckReference() : base(mainRegex)
        {

        }

        public override bool IsValid(object value)
        {
            string input = value as string;
            if (string.IsNullOrWhiteSpace(input))
            {
                return base.IsValid(value);
            }


            //Check if the input is for one of the "special" series.  
            if (seriesSpecific1.IsMatch(input))
            {
                bool isMatch = seriesSpecific2.IsMatch(input);
                return isMatch;
            }
            else
            {
                return base.IsValid(value);
            }
        }
    }
}

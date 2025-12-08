using book_a_reading_room_visit.web.Helper;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace book_a_reading_room_visit.web.Validators
{
    public class CheckReference : RegularExpressionAttribute
    {
        private static readonly Regex seriesSpecific1 = new Regex(@"^(CP ?2[456]|IR ?(12[14-9]|13[0-5])|PRO ?(3[01]|41|66))");
        private static readonly Regex seriesSpecific2 = new Regex(@"^(CP ?2[456]\/|IR ?(12[14-9]|13[0-5])\/|PRO ?(3[01]|41|66)\/)[0-9]");
        private static readonly Regex parlyRegex = new Regex(Constants.Doc_Ref_Regex_Parly_Archives);

        private static readonly string mainRegex = Constants.Doc_Ref_Regex_General;

        public CheckReference() : base(mainRegex)
        {

        }

        public override bool IsValid(object value)
        {
            string input = (value as string)?.Trim();
            if (string.IsNullOrWhiteSpace(input))
            {
                return base.IsValid(input);
            }


            //Check if the input is for one of the "special" series.  
            if (seriesSpecific1.IsMatch(input))
            {
                bool isMatch = seriesSpecific2.IsMatch(input);
                return isMatch;
            }
            else if (parlyRegex.IsMatch(input))
            {
                return true; 
            }
            else
            {
                return base.IsValid(input);
            }
        }
    }
}

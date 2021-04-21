using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace book_a_reading_room_visit.web.Helper
{
    public class CustomHtmlValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (!string.IsNullOrWhiteSpace(value?.ToString()))
            {
                var inputValue = value?.ToString().Replace("\r", "").Replace("\n", "");
                var regExpression = new Regex(@"^(?!.*<[^>]+>).*", RegexOptions.Multiline);
                if (!regExpression.IsMatch(inputValue))
                {
                    return new ValidationResult(FormatErrorMessage(context.DisplayName));
                }
            }
            return ValidationResult.Success;
        }
    }
}

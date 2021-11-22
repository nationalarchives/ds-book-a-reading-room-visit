using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace book_a_reading_room_visit.web.Helper
{
    public class EmailAttribute
        : RegularExpressionAttribute, IClientValidatable
    {
        public EmailAttribute()
            //updated to llow new domain names in the email addresses, such as james@jamesharrison.productions
            //by increasing the length of characters up to 24
            : base(@"^([a-zA-Z0-9_\-\+\'\.]+)@((\[[0-9]{1,3}" +
            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
            @".)+))([a-zA-Z]{2,24}|[0-9]{1,3})(\]?)$")
        {
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var errorMessage = FormatErrorMessage(metadata.GetDisplayName());

            yield return new EmailValidationRule(errorMessage);
        }
    }

    public class EmailValidationRule : ModelClientValidationRule
    {
        public EmailValidationRule(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ValidationType = "email";
        }
    }
}

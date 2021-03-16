using book_a_reading_room_visit.domain;
using book_a_reading_room_visit.web.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.web.Helper
{
    public class BookingModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata?.Name.ToLower() == "bookingtype")
            {
                return new BinderTypeModelBinder(typeof(BookingTypeModelBinder));
            }
            return null;
        }


    }

    public class BookingTypeModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            var model = bindingContext.ValueProvider.GetValue("bookingtype").FirstOrDefault()?.ToBookingType();
            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}

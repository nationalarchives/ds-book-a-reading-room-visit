using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace book_a_reading_room_visit.web.Helper
{
    public static class CookieExtension
    {
        public static IApplicationBuilder RegisterTNACookieConsent(this IApplicationBuilder app)
        {
            app.Use(async (context, nextMiddleware) =>
            {
                if (!context.Request.Cookies.Keys.Contains(Constants.CookieName))
                {
                    var option = new CookieOptions
                    {
                        Expires = DateTime.UtcNow.AddDays(90),
                        Path = "/",
                        Domain = context.Request.Host.Host == Constants.Localhost ? Constants.Localhost : Constants.TNADomain
                    };
                    var cookiePolicy = new CookiePolicy
                    {
                        Essential = true,
                        Settings = false,
                        Usage = false
                    };
                    var value = JsonConvert.SerializeObject(cookiePolicy); 
                    context.Response.Cookies.Append(Constants.CookieName, value, option);
                }
                await nextMiddleware();
            });
            return app;
        }
    }
}

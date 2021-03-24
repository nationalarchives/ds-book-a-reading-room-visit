using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.web.Helper
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseSecurityHeaderMiddleware(this IApplicationBuilder app)
        {
            app.Use(async (context, nextMiddleware) =>
            {
                context.Response.OnStarting(() =>
                {
                    context.Response.AddHeaderKey("X-XSS-Protection", "1");
                    context.Response.AddHeaderKey("X-Frame-Options", "DENY");
                    context.Response.AddHeaderKey("X-Content-Type-Options", "nosniff");
                    context.Response.AddHeaderKey("X-Permitted-Cross-Domain-Policies", "none");
                    context.Response.AddHeaderKey("Referrer-Policy", "no-referrer");
                    context.Response.AddHeaderKey("Strict-Transport-Security", "max-age=31536000 ; includeSubDomains");
                    context.Response.AddHeaderKey("Expect-CT", "max-age=86400");
                    return Task.FromResult(0);
                });
                await nextMiddleware();
            });
            return app;
        }

        private static void AddHeaderKey(this HttpResponse response, string key, StringValues value)
        {
            if (!response.Headers.ContainsKey(key))
                response.Headers.Add(key, value);
        }
    }
}

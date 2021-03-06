using System;
using System.Net.Http.Headers;
using book_a_reading_room_visit.web.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace book_a_reading_room_visit.web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddHttpClient<IAvailabilityService, AvailabilityService>(c =>
            {
                c.BaseAddress = new Uri(Environment.GetEnvironmentVariable("KBS_WebApi_URL"));
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddScoped<AvailabilityService>();
            services.AddMvc(options => 
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseRouting();
            var rootPath = Environment.GetEnvironmentVariable("KBS_Root_Path");
            if (!string.IsNullOrWhiteSpace(rootPath))
            {
                app.Use((context, next) =>
                {
                    context.Request.PathBase = new PathString(rootPath);
                    return next();
                });
            }
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "",
                    new { controller = "Home", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "availability",
                    pattern: "{ordertype}/availability",
                    new { controller = "Home", action = "Availability" });

                endpoints.MapControllerRoute(
                    name: "secure-booking",
                    pattern: "{ordertype}/secure-booking",
                    new { controller = "Booking", action = "SecureBooking" });

                endpoints.MapControllerRoute(
                    name: "booking-confirmation",
                    pattern: "{ordertype}/booking-confirmation",
                    new { controller = "Booking", action = "BookingConfirmation" });

                endpoints.MapControllerRoute(
                    name: "order-documents",
                    pattern: "{ordertype}/order-documents",
                    new { controller = "DocumentOrder", action = "OrderDocuments" });

                endpoints.MapControllerRoute(
                    name: "document-order",
                    pattern: "{ordertype}/document-order/{bookingreference}",
                    new { controller = "DocumentOrder", action = "DocumentOrder" });
            });
        }
    }
}

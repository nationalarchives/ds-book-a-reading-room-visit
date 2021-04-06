using System;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Xml;
using book_a_reading_room_visit.web.Helper;
using book_a_reading_room_visit.web.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace book_a_reading_room_visit.web
{
    public class Startup
    {
        private readonly IWebHostEnvironment _currentEnvironment;
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            _currentEnvironment = webHostEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
            {
                options.ModelBinderProviders.Insert(0, new EnumModelBinderProvider());
            });
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddDataProtection().PersistKeysToAWSSystemsManager("/KBS-Web/DataProtection");

            services.AddHttpClient<IAvailabilityService, AvailabilityService>(c =>
            {
                c.BaseAddress = new Uri(Environment.GetEnvironmentVariable("KBS_WebApi_URL"));
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddHttpClient<IBookingService, BookingService>(c =>
            {
                c.BaseAddress = new Uri(Environment.GetEnvironmentVariable("KBS_WebApi_URL"));
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            var wcfAdvanceOrderServiceEndPoint = Environment.GetEnvironmentVariable("AdvanceOrderServiceEndPoint");
            services.AddSingleton(s => new ChannelFactory<IAdvancedOrderService>(new BasicHttpBinding(), new EndpointAddress(wcfAdvanceOrderServiceEndPoint)));

            var wcfBulkOrderServiceEndPoint = Environment.GetEnvironmentVariable("BulkOrderServiceEndPoint");
            services.AddSingleton(s => new ChannelFactory<IBulkOrdersService>(new BasicHttpBinding(), new EndpointAddress(wcfBulkOrderServiceEndPoint)));

            services.AddScoped<ValidateDocumentOrder>();
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).SetCompatibilityVersion(CompatibilityVersion.Latest);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            var config = Configuration.GetAWSLoggingConfigSection();
            loggerFactory.AddAWSProvider(config, formatter: (logLevel, message, exception) => $"[{DateTime.UtcNow}] {logLevel}: {message}");

            var logger = loggerFactory.CreateLogger<Startup>();
            logger.LogInformation($"KBS - Web UI");

            app.UseSecurityHeaderMiddleware();
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
                    pattern: "{bookingtype}/availability",
                    new { controller = "Home", action = "Availability" });

                endpoints.MapControllerRoute(
                    name: "secure-booking",
                    pattern: "{bookingtype}/secure-booking",
                    new { controller = "Booking", action = "SecureBooking" });

                endpoints.MapControllerRoute(
                    name: "check-ticket",
                    pattern: "{bookingtype}/secure-booking/check-ticket",
                    new { controller = "Booking", action = "CheckReaderTicket" });

                endpoints.MapControllerRoute(
                    name: "booking-confirmation",
                    pattern: "{bookingtype}/booking-confirmation",
                    new { controller = "Booking", action = "BookingConfirmation" });

                endpoints.MapControllerRoute(
                    name: "cancel-provision",
                    pattern: "{bookingtype}/cancel-provision",
                    new { controller = "Booking", action = "CancelProvision" });

                endpoints.MapControllerRoute(
                    name: "cancel-booking",
                    pattern: "{bookingtype}/cancel-booking",
                    new { controller = "Booking", action = "CancelBooking" });

                endpoints.MapControllerRoute(
                    name: "cancellation-confirmation",
                    pattern: "{bookingtype}/cancellation-confirmation",
                    new { controller = "Booking", action = "CancellationConfirmation" });

                endpoints.MapControllerRoute(
                    name: "order-documents",
                    pattern: "{bookingtype}/order-documents/{readerticket}/{bookingreference}",
                    new { controller = "DocumentOrder", action = "OrderDocuments" });

                endpoints.MapControllerRoute(
                    name: "document-order",
                    pattern: "{bookingtype}/document-order/{readerticket}/{bookingreference}",
                    new { controller = "DocumentOrder", action = "OrderComplete" });

                endpoints.MapControllerRoute(
                    name: "return-to-booking",
                    pattern: "return-to-booking",
                    new { controller = "Booking", action = "ReturnToBooking" });

                endpoints.MapControllerRoute(
                    name: "continue-later",
                    pattern: "continue-later",
                    new { controller = "Booking", action = "ContinueLater" });

                endpoints.MapControllerRoute(
                   name: "thank-you",
                   pattern: "thank-you",
                   new { controller = "Booking", action = "ThankYou" });

                endpoints.MapControllerRoute(
                   name: "error",
                   pattern: "error",
                   new { controller = "Home", action = "Error" });
            });
        }
    }
}

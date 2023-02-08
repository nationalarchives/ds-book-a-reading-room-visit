using Amazon.Extensions.NETCore.Setup;
using book_a_reading_room_visit.web.Helper;
using book_a_reading_room_visit.web.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.ServiceModel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    options.ModelBinderProviders.Insert(0, new EnumModelBinderProvider());
});
builder.Services.AddHttpContextAccessor();

// Get the AWS profile information from configuration providers
AWSOptions awsOptions = builder.Configuration.GetAWSOptions();
// Configure AWS service clients to use these credentials
builder.Services.AddDefaultAWSOptions(awsOptions);
builder.Services.AddDataProtection().PersistKeysToAWSSystemsManager("/KBS-API/DataProtection");

// Add NLoging to the container.
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Warning);

builder.Services.AddHttpClient<IAvailabilityService, AvailabilityService>(c =>
{
    c.BaseAddress = new Uri(Environment.GetEnvironmentVariable("KBS_WebApi_URL"));
    c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddHttpClient<IBookingService, BookingService>(c =>
{
    c.BaseAddress = new Uri(Environment.GetEnvironmentVariable("KBS_WebApi_URL"));
    c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

var wcfAdvanceOrderServiceEndPoint = Environment.GetEnvironmentVariable("AdvanceOrderServiceEndPoint");
builder.Services.AddSingleton(s => new ChannelFactory<IAdvancedOrderService>(new BasicHttpBinding(), new EndpointAddress(wcfAdvanceOrderServiceEndPoint)));

var wcfBulkOrderServiceEndPoint = Environment.GetEnvironmentVariable("BulkOrderServiceEndPoint");
builder.Services.AddSingleton(s => new ChannelFactory<IBulkOrdersService>(new BasicHttpBinding(), new EndpointAddress(wcfBulkOrderServiceEndPoint)));

builder.Services.AddScoped<ValidateDocumentOrder>();
builder.Services.AddMvc(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
}

app.RegisterTNACookieConsent();
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
        pattern: "{bookingtype}/continue-later/{bookingreference}",
        new { controller = "DocumentOrder", action = "ContinueLater" });

    endpoints.MapControllerRoute(
       name: "thank-you",
       pattern: "{bookingtype}/thank-you",
       new { controller = "Booking", action = "ThankYou" });

    endpoints.MapControllerRoute(
       name: "error",
       pattern: "error",
       new { controller = "Home", action = "Error" });
});

app.Run();

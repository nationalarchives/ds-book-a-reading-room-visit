using Amazon.Extensions.NETCore.Setup;
using Amazon.SimpleEmail;
using book_a_reading_room_visit.api.Logging;
using book_a_reading_room_visit.api.Service;
using book_a_reading_room_visit.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog.Web;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Get the AWS profile information from configuration providers
AWSOptions awsOptions = builder.Configuration.GetAWSOptions();
// Configure AWS service clients to use these credentials
builder.Services.AddDefaultAWSOptions(awsOptions);
builder.Services.AddDataProtection().PersistKeysToAWSSystemsManager("/KBS-API/DataProtection");
builder.Services.AddAWSService<IAmazonSimpleEmailService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// Add NLoging to the container.
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Warning);
builder.Host.UseNLog();

var logger = NLogHelper.ConfigureLogger();

builder.Services.AddDbContext<BookingContext>(opt =>
    opt.UseSqlServer(Environment.GetEnvironmentVariable("KewBookingConnection"))
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddMemoryCache();

string sRecordCopyingUrl = Environment.GetEnvironmentVariable("RecordCopying_WebApi_URL") ?? string.Empty;

if (String.IsNullOrEmpty(sRecordCopyingUrl))
{
    throw new ApplicationException("RecordCopying WebApi URL must be provided via the RecordCopying_WebApi_URL environment variable.");
}

builder.Services.AddHttpClient<IBankHolidayAPI, BankHolidayAPI>(c =>
{
    c.BaseAddress = new Uri(sRecordCopyingUrl);
    c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    c.DefaultRequestHeaders.Host = Environment.GetEnvironmentVariable("RecordCopying_Header");
});
builder.Services.AddScoped<IWorkingDayService, WorkingDayService>();
builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();
builder.Services.AddScoped<IBookingService, BookingService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Book a reading room visit-api", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Book a reading room visit-api v1"));
}
app.ConfigureExceptionHandler(logger);

app.UseRouting();
app.MapControllers();
app.Run();

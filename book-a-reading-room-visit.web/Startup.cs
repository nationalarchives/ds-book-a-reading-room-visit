using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");

                endpoints.MapControllerRoute(
                    name: "availability",
                    pattern: "{controller=Home}/{action=Availability}");

                endpoints.MapControllerRoute(
                    name: "secure-booking",
                    pattern: "{controller=Booking}/{action=SecureBooking}");

                endpoints.MapControllerRoute(
                    name: "booking-confirmation",
                    pattern: "{controller=Booking}/{action=BookingConfirmation}");

                endpoints.MapControllerRoute(
                    name: "order-documents",
                    pattern: "{controller=DocumentOrder}/{action=OrderDocuments}");

                endpoints.MapControllerRoute(
                    name: "document-order",
                    pattern: "{controller=DocumentOrder}/{action=DocumentOrder}");
            });
        }
    }
}

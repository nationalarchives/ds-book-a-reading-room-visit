using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Slack;
using NLog.Targets;
using NLog.Web;
using System;
using System.Linq;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace book_a_reading_room_visit.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nLog.config").GetCurrentClassLogger();
            try
            {
                SetNLogSlackTarget();
                LogManager.ConfigurationReloaded += (sender, e) =>
                {
                    //Re apply if config reloaded
                    SetNLogSlackTarget();
                };

                logger.Debug("Book a Reading Room visit API Starting Up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (System.Exception e)
            {
                logger.Error(e, "Book a Reading Room visit API is stopping due to an exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseNLog();

        private static void SetNLogSlackTarget()
        {
            string slackWebhookUrl = Environment.GetEnvironmentVariable("KBS_SLACK_WEBHOOK");

            if(String.IsNullOrEmpty(slackWebhookUrl))
            {
                throw new ApplicationException("Slack webhook URL must be provided via the KBS_SLACK_WEBHOOK environment variable.");
            }
            
            var configuration = LogManager.Configuration;
            var targets = configuration.AllTargets;

            // N.B. This returns null so have to find all targets and then cast!
            //SlackTarget slackTarget = configuration.FindTargetByName<SlackTarget>("slackTarget");
            SlackTarget slackTarget = (SlackTarget)targets.First(t => t.GetType() == typeof(SlackTarget));
            
            Target target = configuration.FindTargetByName("slackTarget");

            slackTarget.WebHookUrl = slackWebhookUrl;
            LogManager.Configuration = configuration;
        }
    }
}

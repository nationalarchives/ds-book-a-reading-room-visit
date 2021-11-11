using NLog;
using NLog.Slack;
using NLog.Targets;
using System;
using System.Linq;

namespace book_a_reading_room_visit.api.Logging
{
    public static class NLogHelper
    {

        public static void ConfigureLogger()
        {

            LogFactory logFactory = NLog.Web.NLogBuilder.ConfigureNLog("nLog.config");

            SetNLogSlackTarget();
            SetNLogSqlTarget();
            LogManager.ConfigurationReloaded += (sender, e) =>
            {
                //Re apply if config reloaded
                SetNLogSlackTarget();
                SetNLogSqlTarget();
            };
        }

        private static void SetNLogSlackTarget()
        {
            string slackWebhookUrl = Environment.GetEnvironmentVariable("KBS_SLACK_WEBHOOK");

            if (String.IsNullOrEmpty(slackWebhookUrl))
            {
                throw new ApplicationException("Slack webhook URL must be provided via the KBS_SLACK_WEBHOOK environment variable.");
            }

            var configuration = LogManager.Configuration;
            var targets = configuration.AllTargets;

            // N.B. This returns null so have to find all targets and then cast!
            //SlackTarget slackTarget = configuration.FindTargetByName<SlackTarget>("slackTarget");
            SlackTarget slackTarget = (SlackTarget)targets.First(t => t.GetType() == typeof(SlackTarget));
            slackTarget.WebHookUrl = slackWebhookUrl;
            LogManager.Configuration = configuration;
        }

        private static void SetNLogSqlTarget()
        {
            string nlogSqlConnectionString = Environment.GetEnvironmentVariable("NLOG_SQL_CONNECTION");

            if (String.IsNullOrEmpty(nlogSqlConnectionString))
            {
                throw new ApplicationException("NLog SQL connection string must be provided via the NLOG_SQL_CONNECTION environment variable.");
            }

            var configuration = LogManager.Configuration;
            var targets = configuration.AllTargets;

            // N.B. This returns null so have to find all targets and then cast!
            //DatabaseTarget databaseTarget = configuration.FindTargetByName<DatabaseTarget>("sqlServerTarget");
            DatabaseTarget databaseTarget = (DatabaseTarget)targets.First(t => t.GetType() == typeof(DatabaseTarget));
            databaseTarget.ConnectionString = nlogSqlConnectionString;
            LogManager.Configuration = configuration;
        }
    }
}

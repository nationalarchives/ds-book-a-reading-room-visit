using NLog;
using NLog.Slack;
using NLog.Targets;

namespace book_a_reading_room_visit.web.Logging
{
    public static class NLogHelper
    {
        public static Logger ConfigureLogger()
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
            return logFactory.GetCurrentClassLogger();
        }

        private static void SetNLogSlackTarget()
        {
            string slackWebhookUrl = Environment.GetEnvironmentVariable("KBS_SLACK_WEBHOOK") ?? string.Empty;

            if (String.IsNullOrEmpty(slackWebhookUrl))
            {
                throw new ApplicationException("Slack webhook URL must be provided via the KBS_SLACK_WEBHOOK environment variable.");
            }

            var configuration = LogManager.Configuration;
            var targets = configuration.AllTargets;

            SlackTarget slackTarget = (SlackTarget)targets.First(t => t.GetType() == typeof(SlackTarget));
            slackTarget.WebHookUrl = slackWebhookUrl;
            LogManager.Configuration = configuration;
        }

        private static void SetNLogSqlTarget()
        {
            string nlogSqlConnectionString = Environment.GetEnvironmentVariable("NLOG_SQL_CONNECTION") ?? string.Empty;

            if (String.IsNullOrEmpty(nlogSqlConnectionString))
            {
                throw new ApplicationException("NLog SQL connection string must be provided via the NLOG_SQL_CONNECTION environment variable.");
            }

            var configuration = LogManager.Configuration;
            var targets = configuration.AllTargets;

            DatabaseTarget databaseTarget = (DatabaseTarget)targets.First(t => t.GetType() == typeof(DatabaseTarget));
            databaseTarget.ConnectionString = nlogSqlConnectionString;
            LogManager.Configuration = configuration;
        }
    }
}

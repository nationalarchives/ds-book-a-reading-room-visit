using NLog;
using NLog.Slack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Logging
{
    public static class NLogHelper
    {

        public static void ConfigureLogger()
        {

            LogFactory logFactory = NLog.Web.NLogBuilder.ConfigureNLog("nLog.config");

            SetNLogSlackTarget();
            LogManager.ConfigurationReloaded += (sender, e) =>
            {
                //Re apply if config reloaded
                SetNLogSlackTarget();
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
    }
}

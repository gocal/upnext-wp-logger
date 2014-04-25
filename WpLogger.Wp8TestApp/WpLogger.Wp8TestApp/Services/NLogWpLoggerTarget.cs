using NLog;
using NLog.Targets;
using WpLogger.DataContract.Services;

namespace WpLogger.Wp8TestApp.Services
{
    [Target("wp_logger")]
    public sealed class NLogWpLoggerTarget : TargetWithLayout
    {
        #region Fields

        private readonly WpLoggerService wpLoggerService;

        #endregion

        #region Constructors and Destructors

        public NLogWpLoggerTarget()
        {
            wpLoggerService = new WpLoggerService();
        }

        #endregion

        #region Methods

        protected override void Write(LogEventInfo logEvent)
        {
            wpLoggerService.SendLog(logEvent.Level.Name, logEvent.LoggerName, logEvent.FormattedMessage);
        }

        #endregion
    }
}
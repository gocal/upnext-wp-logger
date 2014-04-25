using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.Targets;

namespace WpLogger.Wp8TestApp.Services
{
    [Target("wp_logger")] 
    public sealed class NLogWpLoggerTarget : TargetWithLayout
    {
        private readonly WpLoggerService wpLoggerService;

        public NLogWpLoggerTarget()
        {
            wpLoggerService = new WpLoggerService();    
        }

        protected override void Write(LogEventInfo logEvent)
        {
            var logMessage = this.Layout.Render(logEvent);
            wpLoggerService.SendLog(logEvent.LoggerName, logMessage);    
        }
    }
}

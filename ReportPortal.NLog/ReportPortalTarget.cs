using System.Collections.Generic;
using ReportPortal.Shared;
using NLog.Targets;
using ReportPortal.Shared.Execution.Logging;

namespace ReportPortal.Logging
{
    /// <summary>
    /// NLog custom target for reporting logs directly to Report Portal.
    /// Logs will be viewable under current test item from shared context.
    /// </summary>
    [Target("ReportPortal")]
    public class ReportPortalTarget : TargetWithLayout
    {
        protected Dictionary<NLog.LogLevel, LogMessageLevel> LevelMap = new Dictionary<NLog.LogLevel, LogMessageLevel>();

        public ReportPortalTarget()
        {
            LevelMap[NLog.LogLevel.Fatal] = LogMessageLevel.Fatal;
            LevelMap[NLog.LogLevel.Error] = LogMessageLevel.Error;
            LevelMap[NLog.LogLevel.Warn] = LogMessageLevel.Warning;
            LevelMap[NLog.LogLevel.Info] = LogMessageLevel.Info;
            LevelMap[NLog.LogLevel.Debug] = LogMessageLevel.Debug;
            LevelMap[NLog.LogLevel.Trace] = LogMessageLevel.Trace;
        }

        protected override void Write(NLog.LogEventInfo logEvent)
        {
            var level = LogMessageLevel.Info;
            if (LevelMap.ContainsKey(logEvent.Level))
            {
                level = LevelMap[logEvent.Level];
            }

            var logMessage = new LogMessage(Layout.Render(logEvent));
            logMessage.Time = logEvent.TimeStamp.ToUniversalTime();
            logMessage.Level = level;

            Context.Current.Log.Message(logMessage);
        }
    }
}

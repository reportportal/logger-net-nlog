using System.Collections.Generic;
using ReportPortal.Shared;
using NLog.Targets;
using ReportPortal.Client.Abstractions.Responses;
using ReportPortal.Client.Abstractions.Requests;

namespace ReportPortal.Logging
{
    /// <summary>
    /// NLog custom target for reporting logs directly to Report Portal.
    /// Logs will be viewable under current test item from shared context.
    /// </summary>
    [Target("ReportPortal")]
    public class ReportPortalTarget : TargetWithLayout
    {
        protected Dictionary<NLog.LogLevel, LogLevel> LevelMap = new Dictionary<NLog.LogLevel, LogLevel>();

        public ReportPortalTarget()
        {
            LevelMap[NLog.LogLevel.Fatal] = LogLevel.Fatal;
            LevelMap[NLog.LogLevel.Error] = LogLevel.Error;
            LevelMap[NLog.LogLevel.Warn] = LogLevel.Warning;
            LevelMap[NLog.LogLevel.Info] = LogLevel.Info;
            LevelMap[NLog.LogLevel.Debug] = LogLevel.Debug;
            LevelMap[NLog.LogLevel.Trace] = LogLevel.Trace;
        }

        protected override void Write(NLog.LogEventInfo logEvent)
        {
            var level = LogLevel.Info;
            if (LevelMap.ContainsKey(logEvent.Level))
            {
                level = LevelMap[logEvent.Level];
            }

            Log.Message(new CreateLogItemRequest
            {
                Level = level,
                Time = logEvent.TimeStamp.ToUniversalTime(),
                Text = Layout.Render(logEvent)
            });
        }
    }
}

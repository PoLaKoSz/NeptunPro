using NLog;
using NLog.Config;
using NLog.Targets;

namespace NeptunPro
{
    public abstract class Logger
    {
        internal readonly static NLog.Logger Log;



        static Logger()
        {
            var config = new LoggingConfiguration();

            var logfile = new FileTarget("logfile") { FileName = "file.txt" };
            var logconsole = new ConsoleTarget("logconsole");

            config.AddRuleForAllLevels("logfile");

            LogManager.Configuration = config;

            Log = LogManager.GetCurrentClassLogger();
        }
    }
}

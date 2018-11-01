using NLog;
using NLog.Config;
using NLog.Targets;

namespace NeptunPro
{
    public abstract class Log
    {
        internal readonly static Logger Instance;



        static Log()
        {
            var config = new LoggingConfiguration();

            var logfile = new FileTarget("logfile") { FileName = "file.txt" };
            var logconsole = new ConsoleTarget("logconsole");

            config.AddRuleForAllLevels("logfile");

            LogManager.Configuration = config;

            Instance = LogManager.GetCurrentClassLogger();
        }
    }
}

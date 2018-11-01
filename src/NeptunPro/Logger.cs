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

            var logfile = new FileTarget("logfile")
            {
                Layout = "${longdate}\t${level}\t${message}\t${exception}",
                FileName = "${basedir}/logs/logfile.txt",
                ArchiveFileName = "${basedir}/logs/log.{#}.txt",
                ArchiveEvery = FileArchivePeriod.Day,
                ArchiveNumbering = ArchiveNumberingMode.DateAndSequence,
                MaxArchiveFiles = 7
            };

            config.AddRuleForAllLevels(logfile);

            LogManager.Configuration = config;

            Log = LogManager.GetCurrentClassLogger();
        }
    }
}

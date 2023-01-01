using NLog;
using NLog.Config;

namespace Tasko.Logger.Services
{
    public static class LogService
    {
        public static ILogger GetLogger()
        {
            var fileName = Path.GetFullPath(@"../../Tasko-Backend/Tasko.Logger/NLog.config");
            LogManager.Configuration = new XmlLoggingConfiguration(fileName);
            return LogManager.GetCurrentClassLogger();
        }
    }
}

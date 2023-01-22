using NLog.Config;

namespace Tasko.Logger.Services;
public static class LogService
{
    public static ILogger GetLogger()
    {
        string fileName = string.Empty; 
#if DEBUG
        fileName = Path.GetFullPath(@"../../Libraries/Tasko.Logger/NLog.config");
#endif
#if !DEBUG
            fileName = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}/NLog.config";
#endif
        LogManager.Configuration = new XmlLoggingConfiguration(fileName);
        return LogManager.GetCurrentClassLogger();
    }
}
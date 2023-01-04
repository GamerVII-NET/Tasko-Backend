using Tasko.AuthService.Infrastructure.Extensions;
using Tasko.Logger.Services;

var logger = LogService.GetLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.RegisterBuilder();
    var application = builder.Build();
    application.RegisterApplication(logger);
    application.Run();
}
catch (Exception ex)
{
    logger.Error(ex, $"{Assembly.GetCallingAssembly().GetName().Name} stopped because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}

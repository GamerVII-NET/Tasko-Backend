using Tasko.Logger.Services;

var logger = LogService.GetLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.RegisterBuilder();
    var app = builder.Build();
    app.RegisterApplication(logger);
    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, $"{Assembly.GetCallingAssembly().GetName().Name} stopped because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}

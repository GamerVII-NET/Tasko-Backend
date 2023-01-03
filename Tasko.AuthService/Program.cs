using Tasko.AuthService.Infrastructure.Extensions;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.RegisterBuilder();
    var application = builder.Build();
    application.RegisterApplication();
    application.Run();
}
catch (Exception)
{

	throw;
}
finally
{

}

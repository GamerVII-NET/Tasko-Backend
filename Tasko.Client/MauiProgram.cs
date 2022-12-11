using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tasko.Client.Helpers;
using Tasko.Client.Services;


namespace Tasko.Client
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
                   .ConfigureFonts(fonts =>
                   {
                       fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                   });
            builder.Logging.AddDebug();

            ConfigureService.SettingServices(builder.Services);

            var host = builder.Build();

            return host;
        }
    }
}
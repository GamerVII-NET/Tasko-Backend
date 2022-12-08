﻿namespace Tasko.Client.Services
{
    internal static class ConfigureService
    {
        internal static void SettingServices(IServiceCollection services)
        {
            services.AddMauiBlazorWebView();
            #if DEBUG
            services.AddBlazorWebViewDeveloperTools();
            #endif
            services.AddAuthorizationCore();
            services.AddScoped<AuthStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<AuthStateProvider>());
            services.AddHttpClient("base", client => client.BaseAddress = new Uri("http://87.249.49.56:7177/api"));
            services.AddTransient<ILoginViewModel, LoginViewModel>();
        }
    }
}

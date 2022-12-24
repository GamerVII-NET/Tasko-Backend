namespace Tasko.Client.Services
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

            services.AddHttpClient("base", client => client.BaseAddress = new Uri("http://87.249.49.56:8001"))
                .AddHttpMessageHandler<ValidateHeaderHandler>();

            services.AddScoped<AuthStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<AuthStateProvider>());
            services.AddScoped<ILoginViewModel, IndexViewModel>();
            services.AddScoped<IBoardViewModel, BoardViewModel>();

            services.AddScoped<IBoardsService, BoardsService>();
            services.AddScoped<IUserService, UserService>();

            services.AddTransient<IRegisterViewModel, RegisterViewModel>();
            services.AddTransient<ValidateHeaderHandler>();

        }
    }
}

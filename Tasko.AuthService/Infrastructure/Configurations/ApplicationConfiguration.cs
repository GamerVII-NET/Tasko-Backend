using Microsoft.AspNetCore.Authentication.JwtBearer;
using MongoDB.Driver;
using Tasko.AuthService.Infrastructure.Api;
using Tasko.AuthService.Infrastructure.Repositories;
using Tasko.General.Commands;
using Tasko.General.Extensions.Jwt;
using Tasko.General.Interfaces;

namespace Tasko.AuthService.Infrastructure.Configurations;

internal static class ApplicationConfiguration
{
    internal static void RegisterBuilder(this WebApplicationBuilder builder, IMongoDatabase dataContext)
    {
        //builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options => Jwt.GenerateConfig(ref options, builder.Configuration.GetJwtValidationParameter()));

        builder.Services.AddSingleton(dataContext);
        builder.Services.AddScoped<IAuthRepository, AuthRepository>();
        builder.Services.AddTransient<IApi, AuthApi>();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
    }
    internal static void RegisterApplication(this WebApplication application, WebApplicationBuilder builder)
    {
        if (application.Environment.IsProduction())
        {
            application.Urls.Add("http://87.249.49.56:7177");
        }
        application.UseAuthentication();
        application.UseAuthorization();
    }
}

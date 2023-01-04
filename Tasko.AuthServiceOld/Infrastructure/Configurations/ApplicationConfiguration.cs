﻿using Tasko.AuthService.Infrastructure.Api;
using Tasko.AuthService.Infrastructure.Repositories;

namespace Tasko.AuthService.Infrastructure.Configurations;

internal static class ApplicationConfiguration
{
    internal static void RegisterBuilder(this WebApplicationBuilder builder, IMongoDatabase dataContext)
    {
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddFluentValidation(validation => validation.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options => Jwt.GenerateConfig(ref options, builder.Configuration.GetJwtValidationParameter()));

        builder.Services.AddSingleton(dataContext);
        builder.Services.AddScoped<IAuthRepository, AuthRepository>();
        builder.Services.AddTransient<General.Interfaces.IRouteHandler, AuthApi>();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
        builder.Services.AddSwaggerGen();
    }
    internal static void RegisterApplication(this WebApplication application)
    {
        if (application.Environment.IsDevelopment())
        {
            application.UseSwagger();
            application.UseSwaggerUI();
        }
        //if (application.Environment.IsProduction())
        //{
        //    application.Urls.Add("http://87.249.49.56:7177");
        //}
        application.UseAuthentication();
        application.UseAuthorization();
    }
}
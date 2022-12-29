using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Tasko.General.Commands;
using Tasko.General.Extensions.Jwt;

namespace Tasko.General.Extensions;

public static class ApplicationExtensions
{
    public static void RegisterMongoDataBase(this WebApplicationBuilder builder)
    {
#if DEBUG
        builder.Configuration.AddJsonFile("appsettings.json");
#endif
#if !DEBUG
        builder.SetSettingFile(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "appsettings.json"); //If project on docker
#endif
        var dbConnectionString = builder.Configuration.GetMongoConnectionString();
        var dbName = builder.Configuration.GetMongoDatabaseName();
        var databaseContext = Mongo.GetMongoDataConext(dbConnectionString, dbName);

        builder.Services.AddSingleton(databaseContext);
    }

    public static void RegisterJwtBearerAuth(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => Commands.Jwt.GenerateConfig(ref options, builder.Configuration.GetJwtValidationParameter()));
    }

    public static void RegisterSwaggerAuthorization(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
         {
             s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
             {
                 In = ParameterLocation.Header,
                 Description = "Insert JWT token",
                 Name = "Authorization",
                 Type = SecuritySchemeType.Http,
                 BearerFormat = "JWT",
                 Scheme = "bearer",
             });
             s.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[]{}
                }
            });
         });
    }
}
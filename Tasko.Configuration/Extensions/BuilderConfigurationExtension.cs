using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Tasko.Mongo.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Tasko.Jwt.Services;
using Tasko.Jwt.Extensions;
using Tasko.Mongo.Extensions;

namespace Tasko.Configuration.Extensions
{
    public static class BuilderConfigurationExtension
    {
        public static void SetSettingFile(this WebApplicationBuilder builder, string relativePath, string fileName)
        {
            var absolutePath = Path.GetFullPath(relativePath);
            builder.Configuration
                .SetBasePath(absolutePath)
                .AddJsonFile(fileName);
        }
        public static void AddMongoDbContext(this WebApplicationBuilder builder)
        {
#if DEBUG
            builder.Configuration.AddJsonFile("appsettings.json");
#endif
#if !DEBUG
        builder.SetSettingFile(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "appsettings.json"); //If project on docker
#endif
            var dbConnectionString = builder.Configuration.GetMongoConnectionString();
            var dbName = builder.Configuration.GetMongoDatabaseName();
            var databaseContext = MongoServices.GetMongoDataConext(dbConnectionString, dbName);

            builder.Services.AddSingleton(databaseContext);
        }
        public static void AddJwtBearerAuth(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => JwtServices.GenerateConfig(ref options, builder.Configuration.GetValidationParameter()));
        }
        public static void AddSwaggerJwtAuthorization(this IServiceCollection services)
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
}

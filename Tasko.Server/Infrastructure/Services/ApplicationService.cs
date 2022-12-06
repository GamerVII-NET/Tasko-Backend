using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Tasko.Server.Infrastructure.API.Interfaces;
using Tasko.Server.Infrastructure.API.Providers;
using Tasko.Server.Infrastructure.Helpers;
using Tasko.Server.Repositories.Interfaces;
using Tasko.Server.Repositories.Providers;

namespace Tasko.Server.Services;

internal static class ApplicationService
{

    internal static IMongoDatabase GetMongoDataConext(string connection, string databaseName)
    {
        var mongoClient = new MongoClient(connection);

        var databaseContext = mongoClient.GetDatabase(databaseName);
        return databaseContext;
    }
    internal static void RegisterBuilder(this WebApplicationBuilder builder, IMongoDatabase dataContext)
    {
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options => AddJwtBearer.GenerateConfig(options, builder));

        builder.Services.AddSingleton(dataContext);
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IApi, UserApi>();
        builder.Services.AddTransient<IApi, AuthApi>();

        builder.Services.AddSwaggerGen(s =>
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
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
    }
    internal static void RegisterApplication(this WebApplication application, WebApplicationBuilder builder)
    {
        //if (application.Environment.IsDevelopment())
        {
            application.UseSwagger();
            application.UseSwaggerUI();
        }
        application.Urls.Add("http://87.249.49.56:7177");
        application.UseHttpsRedirection();
        application.UseAuthentication();
        application.UseAuthorization();
    }

}

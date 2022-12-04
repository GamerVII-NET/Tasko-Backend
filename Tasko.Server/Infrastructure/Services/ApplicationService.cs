using MongoDB.Driver;
using Tasko.Server.Infrastructure.API.Interfaces;
using Tasko.Server.Infrastructure.API.Providers;
using Tasko.Server.Repositories.Interfaces;
using Tasko.Server.Repositories.Providers;

namespace Tasko.Server.Services;

internal static class ApplicationService
{
    internal static IMongoDatabase GetMongoDataConext(string connection, string databaseName)
    {
        var mongoClient = new MongoClient(connection);
        return mongoClient.GetDatabase(databaseName);
    }

    internal static void RegisterBuilder(this WebApplicationBuilder builder, IMongoDatabase dataContext)
    {
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options => AddJwtBearer
                        .GenerateConfig(options, builder));

        builder.Services.AddSingleton(dataContext);
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IApi, UserApi>();
    }
    internal static void RegisterApplication(this WebApplication application, WebApplicationBuilder builder)
    {
        if (application.Environment.IsDevelopment())
        {
            application.UseSwagger();
            application.UseSwaggerUI();
        }

        application.UseHttpsRedirection();
        application.UseAuthentication();
        application.UseAuthorization();
    }

}

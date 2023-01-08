
using Microsoft.Extensions.Configuration;
using Tasko.Cryptography.Extensions;
using Tasko.Mongo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Tasko.Mongo.Extensions;

public static class MongoDatabaseExtensions
{
    public static void SetSettingFile(this WebApplicationBuilder builder, string relativePath, string fileName)
    {
        var absolutePath = Path.GetFullPath(relativePath);
        builder.Configuration
            .SetBasePath(absolutePath)
            .AddJsonFile(fileName);
    }
    public static string GetMongoConnectionString(this IConfiguration configuration) =>
        configuration["ConnectionStrings:Mongo:Connection"].Decrypt(AesConfiguration.Key, AesConfiguration.IV);
    public static string GetMongoDatabaseName(this IConfiguration configuration) =>
        configuration["ConnectionStrings:Mongo:Database"].Decrypt(AesConfiguration.Key, AesConfiguration.IV);

    public static void AddMongoDbContext(this WebApplicationBuilder builder)
    {
#if DEBUG
        builder.SetSettingFile(@"../../Tasko-Backend/Tasko.Configuration/", "appsettings.json");
#endif
#if !DEBUG
        builder.SetSettingFile(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "appsettings.json");
#endif
        var dbConnectionString = builder.Configuration.GetMongoConnectionString();
        var dbName = builder.Configuration.GetMongoDatabaseName();
        var databaseContext = MongoServices.GetMongoDataConext(dbConnectionString, dbName);

        builder.Services.AddSingleton(databaseContext);
    }


}
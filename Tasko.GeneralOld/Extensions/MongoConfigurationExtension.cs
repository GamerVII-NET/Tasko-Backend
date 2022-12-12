using Microsoft.Extensions.Configuration;
using Tasko.General.Extensions.Crypthography;

namespace Tasko.General.Extensions
{
    public static class MongoConfigurationExtension
    {
        public static string GetMongoConnectionString(this IConfiguration configuration) => configuration["ConnectionStrings:Mongo:Connection"].Decrypt(AesConfiguration.Key, AesConfiguration.IV);
        public static string GetMongoDatabaseName(this IConfiguration configuration) => configuration["ConnectionStrings:Mongo:Database"].Decrypt(AesConfiguration.Key, AesConfiguration.IV);
    }
}

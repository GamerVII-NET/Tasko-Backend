﻿
namespace Tasko.General.Extensions
{
    public static class MongoDatabaseExtensions
    {
        public static string GetMongoConnectionString(this IConfiguration configuration) => 
            configuration["ConnectionStrings:Mongo:Connection"].Decrypt(AesConfiguration.Key, AesConfiguration.IV);
        public static string GetMongoDatabaseName(this IConfiguration configuration) => 
            configuration["ConnectionStrings:Mongo:Database"].Decrypt(AesConfiguration.Key, AesConfiguration.IV);

    }
}

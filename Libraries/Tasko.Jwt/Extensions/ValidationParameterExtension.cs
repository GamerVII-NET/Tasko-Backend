using Tasko.Cryptography.Extensions;

namespace Tasko.Jwt.Extensions;

public static class ValidationParameterExtension
{
    public static ValidationParameter GetValidationParameter(this IConfiguration configuration) =>
           new ValidationParameter
           {
               Key = configuration.GetKey(),
               Audienece = configuration.GetAudience(),
               Issuer = configuration.GetIssuer()
           };

    public static string GetKey(this IConfiguration configuration) => configuration["Jwt:Key"].Decrypt(AesConfiguration.Key, AesConfiguration.IV);
    public static string GetIssuer(this IConfiguration configuration) => configuration["Jwt:Issuer"].Decrypt(AesConfiguration.Key, AesConfiguration.IV);
    public static string GetAudience(this IConfiguration configuration) => configuration["Jwt:Audience"].Decrypt(AesConfiguration.Key, AesConfiguration.IV);
}
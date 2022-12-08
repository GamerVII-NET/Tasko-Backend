using Tasko.Server.Infrastructure.Extensions.AES;
using Tasko.Server.Infrastructure.Services;

namespace Tasko.Server.Infrastructure.Helpers;

public static class AddJwtBearer
{
    public static void GenerateConfig(JwtBearerOptions options, WebApplicationBuilder builder)
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"].Decrypt(AesService.AesKey, AesService.IV),
            ValidAudience = builder.Configuration["Jwt:Audience"].Decrypt(AesService.AesKey, AesService.IV),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"].Decrypt(AesService.AesKey, AesService.IV)))
        };
    }
}
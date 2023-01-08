using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Tasko.Domains.Models.Structural;
using Tasko.Jwt.Models;

namespace Tasko.Jwt.Services
{
    public static class JwtServices
    {
        public static void GenerateConfig(ref JwtBearerOptions options, ValidationParameter validationParmeter)
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = validationParmeter.Issuer,
                ValidAudience = validationParmeter.Audienece,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(validationParmeter.Key))
            };
        }
        public static Guid GetUserGuidFromToken(string token, ValidationParameter validationParameter)
        {
            if (string.IsNullOrEmpty(token)) return Guid.Empty;

            var validationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = validationParameter.SymmetricSecurityKey,
                ValidAudience = validationParameter.Audienece,
                ValidIssuer = validationParameter.Issuer,
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                if (validatedToken != null)
                {
                    var securityToken = (JwtSecurityToken)validatedToken;
                    if (securityToken == null) return Guid.Empty;
                    var userId = Guid.Parse(securityToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

                    return userId;
                }
            }
            catch (Exception e)
            {
                return Guid.Empty;
            }
            return Guid.Empty;
        }
        public static bool VerifyUser(string token, ValidationParameter validationParmeter, IUser user)
        {

            if (string.IsNullOrEmpty(user.Login) && string.IsNullOrEmpty(user.Email)) return false;
            var validationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = validationParmeter.SymmetricSecurityKey,
                ValidAudience = validationParmeter.Audienece,
                ValidIssuer = validationParmeter.Issuer,
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken = null;
            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            }
            catch (SecurityTokenException)
            {
                return false;
            }
            catch (Exception e)
            {
                throw;
            }

            if (validatedToken != null)
            {
                var securityToken = (JwtSecurityToken)validatedToken;
                if (securityToken == null) return false;
                var claimId = securityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (claimId!.Value == $"{user.Id}") return true;
            }
            return false;
        }
        public static Guid VerifyUser(string token, ValidationParameter validationParmeter)
        {

            if (string.IsNullOrEmpty(token)) return Guid.Empty;
            var validationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = validationParmeter.SymmetricSecurityKey,
                ValidAudience = validationParmeter.Audienece,
                ValidIssuer = validationParmeter.Issuer,
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken = null;
            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            }
            catch (SecurityTokenException)
            {
                return Guid.Empty;
            }
            catch (Exception e)
            {
                throw;
            }

            if (validatedToken != null)
            {
                var securityToken = (JwtSecurityToken)validatedToken;

                if (securityToken == null) return Guid.Empty;

                var claimId = Guid.Parse(securityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

                return claimId;

            }
            return Guid.Empty;
        }
        public static string CreateToken(ValidationParameter validationParmeter, IUser user, IEnumerable<IPermission> permissions)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, $"{user.Id}"),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            permissions.ToList().ForEach(permission => claims.Add(new Claim(ClaimTypes.Role, $"{permission.Name}")));

            var now = DateTime.UtcNow;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(validationParmeter.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


#if DEBUG
            var tokenDescriptor = new JwtSecurityToken(
                validationParmeter.Issuer,
                validationParmeter.Audienece,
                claims,
                now,
                now.AddDays(1),
                credentials);
#endif
#if !DEBUG
            var tokenDescriptor = new JwtSecurityToken(
                validationParmeter.Issuer, 
                validationParmeter.Audienece, 
                claims,
                now,
                now.AddMinutes(15),
                credentials);
#endif
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
        public static string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}

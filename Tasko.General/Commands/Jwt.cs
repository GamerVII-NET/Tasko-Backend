﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tasko.Domains.Models.Structural.Providers;
using Tasko.General.Models;

namespace Tasko.General.Commands
{
    // All the code in this file is included in all platforms.
    public static class Jwt
    {
        public static void GenerateConfig(ref JwtBearerOptions options, JwtValidationParameter jwtValidationParmeter)
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtValidationParmeter.Issuer,
                ValidAudience = jwtValidationParmeter.Audienece,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtValidationParmeter.Key))
            };
        }
        public static bool VerifyUser(HttpContext context, JwtValidationParameter jwtValidationParmeter, IUser user)
        {
            string token = context.Request.Headers[Microsoft.Net.Http.Headers.HeaderNames.Authorization].ToString().Replace("Bearer ", string.Empty);
            if (string.IsNullOrEmpty(user.Login) && string.IsNullOrEmpty(user.Email)) return false;
            var validationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = jwtValidationParmeter.SymmetricSecurityKey,
                ValidAudience = jwtValidationParmeter.Audienece,
                ValidIssuer = jwtValidationParmeter.Issuer,
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
                var securityToken = (validatedToken as JwtSecurityToken);
                if (securityToken == null) return false; 
                var claimUser = securityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
                var claimEmail = securityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                var claimGuid = securityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (claimGuid!.Value == $"{user.Id}") return true;
            }
            return false;
        }
        public static string CreateToken(JwtValidationParameter jwtValidationParmeter, IUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, $"{user.Id}"),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var expiryDuration = new TimeSpan(0, 30, 0, 0);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtValidationParmeter.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(jwtValidationParmeter.Issuer, jwtValidationParmeter.Audienece, claims, expires: DateTime.Now.Add(expiryDuration), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
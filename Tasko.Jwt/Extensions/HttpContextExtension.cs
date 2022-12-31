using Microsoft.AspNetCore.Http;

namespace Tasko.Jwt.Extensions
{
    public static class HttpContextExtension
    {
        public static string GetJwtToken(this HttpContext httpContext) =>
        $"{httpContext.Request.Headers[Microsoft.Net.Http.Headers.HeaderNames.Authorization]}".Replace("Bearer ", string.Empty);
    }
}

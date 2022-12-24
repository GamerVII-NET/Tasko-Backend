
namespace Tasko.General.Extensions.Crypthography
{
    public static class HttpContextExtension
    {
        public static string GetJwtToken(this HttpContext httpContext) =>
        $"{httpContext.Request.Headers[Microsoft.Net.Http.Headers.HeaderNames.Authorization]}".Replace("Bearer ", string.Empty);
    }
}

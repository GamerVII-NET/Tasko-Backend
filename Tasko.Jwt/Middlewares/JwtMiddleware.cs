using Microsoft.AspNetCore.Http;
using Tasko.Jwt.Extensions;
using Tasko.Domains.Models.Structural.Providers;

namespace Tasko.Service.Infrasructure.Middlewares;

internal class JwtMiddleware
{
    private readonly RequestDelegate _requestDelegate;

    public JwtMiddleware(RequestDelegate requestDelegate)
    {
        _requestDelegate = requestDelegate;
    }

    public async Task Invoke(HttpContext context, IUserRepository repository)
    {
        var token = context.GetJwtToken();

        var userGuid = repository.GetUserIdFromToken(token);

        if (userGuid != Guid.Empty)
        {
            var user = await repository.FindUserAsync(userGuid);
            context.Items["User"] = user;
        }

        await _requestDelegate(context);
    }

}
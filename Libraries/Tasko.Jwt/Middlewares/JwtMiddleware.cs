using Microsoft.AspNetCore.Http;
using Tasko.Jwt.Services;

namespace Tasko.Service.Infrasructure.Middlewares;

internal class JwtMiddleware
{
    private readonly RequestDelegate _requestDelegate;

    public JwtMiddleware(RequestDelegate requestDelegate)
    {
        _requestDelegate = requestDelegate;
    }

    public async Task Invoke(HttpContext context, ValidationParameter validationParameter)
    {
        var token = context.GetJwtToken();

        var userGuid = JwtServices.GetUserGuidFromToken(token, validationParameter);

        if (userGuid != Guid.Empty)
        {
            //var user = await repository.FindUserAsync(userGuid);
            //context.Items["User"] = user;
        }

        await _requestDelegate(context);
    }

}
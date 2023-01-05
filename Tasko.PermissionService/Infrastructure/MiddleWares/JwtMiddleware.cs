namespace Tasko.Service.MiddleWares;

internal class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ValidationParameter _validationParmeter;

    public JwtMiddleware(RequestDelegate next, ValidationParameter validationParmeter, IConfiguration configuration)
    {
        _next = next;
        _validationParmeter = configuration.GetValidationParameter();
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            await AttachAccountToContext(context, token);
        }

        await _next(context);
    }
    private async Task AttachAccountToContext(HttpContext context, string token)
    {
        var userGuid = Jwt.Services.JwtServices.VerifyUser(token, _validationParmeter);

        if (!userGuid.Equals(Guid.Empty))
            context.Items["UserId"] = userGuid;

    }
}
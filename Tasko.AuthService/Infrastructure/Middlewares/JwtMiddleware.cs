using Tasko.General.Extensions.Crypthography;

namespace Tasko.Service.Infrasructure.Middlewares
{
    internal class JwtMiddleware
    {

        private readonly RequestDelegate _requestDelegate;

        public JwtMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context, IUserRepository repository)
        {
            //var token = context.GetJwtToken();

            //var userGuid = repository.GetUserIdFromToken(token);

            //if (userGuid != Guid.Empty)
            //{
            //    context.Items["User"] = await repository.FindUserAsync(userGuid);
            //}

            await _requestDelegate(context);
        }

    }
}
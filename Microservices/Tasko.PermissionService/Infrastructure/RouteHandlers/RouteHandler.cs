using Tasko.Service.Infrastructure.RequestHandlers;

namespace Tasko.Service.Infrastructure.RouteHandlers
{
    public class RouteHandler : IRouteHandler<WebApplication>
    {
        private ValidationParameter _validationParameter;
        private WebApplication _webApplication;

        public void Initialzie(WebApplication webApplication)
        {
            _validationParameter = webApplication.Configuration.GetValidationParameter();
            _webApplication = webApplication;
            Getters();
            Creators();
            Updaters();
            Deleters();
        }


        public void Getters()
        {
            _webApplication.MapGet("api/permissions", RequestHandler.GetPermissions())
                           .Produces<IEnumerable<IUser>>(StatusCodes.Status200OK)
                           .WithName("Get permissions")
                           .WithTags("Getters");

            //_webApplication.MapGet("api/users/{id}", RequestHandler.FindUser())
            //               .Produces<IUser>(StatusCodes.Status200OK)
            //               .WithName("Find user")
            //               .WithTags("Getters");

            //_webApplication.MapGet("api/users/refresh-tokens/{id}", RequestHandler.GetRefreshTokens(_validationParameter))
            //               .Produces<IEnumerable<IRefreshToken>>(StatusCodes.Status200OK)
            //               .WithName("Get user refres tokens")
            //               .WithTags("Getters");


        }
        public void Creators()
        {
            _webApplication.MapPost("api/permissions", RequestHandler.CreatePermission(_validationParameter))
                           .Produces<IEnumerable<IUser>>(StatusCodes.Status200OK)
                           .WithName("Create permission")
                           .WithTags("Creators");


        }
        private void Updaters()
        {
            //_webApplication.MapPut("api/users", RequestHandler.UpdateUser(_validationParameter))
            //               .Produces(StatusCodes.Status200OK)
            //               .WithName("Update user")
            //               .WithTags("Updaters");
        }
        private void Deleters()
        {
            //_webApplication.MapDelete("api/users", RequestHandler.DeleteUser(_validationParameter))
            //               .Produces<IEnumerable<IUser>>(StatusCodes.Status200OK)
            //               .WithName("Delete user")
            //               .WithTags("Deleters");
        }
    }
}
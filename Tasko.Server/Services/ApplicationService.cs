using Tasko.Server.Services.API;

namespace Tasko.Server.Services;

internal static class ApplicationService
{

    internal static void RegisterApplication(this WebApplication application, WebApplicationBuilder builder)
    {
        if (application.Environment.IsDevelopment())
        {
            application.UseSwagger();
            application.UseSwaggerUI();
        }

        //application.Urls.Add("http://87.249.49.56:7177");
        //application.Urls.Add("http://localhost:7177");

        application.UseHttpsRedirection();
        application.UseAuthentication();
        application.UseAuthorization();

        application.MapGet("/api/v1/users", UserService.GetUsersList());
        application.MapPost("/api/v1/users/register", UserService.CreateUser(builder));

        application.MapGet("/", () =>
        {
            return "Hello, It's Tasko";
        });
    }

}

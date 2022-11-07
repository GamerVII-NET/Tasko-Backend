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

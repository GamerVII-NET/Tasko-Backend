using Tasko.Server.Services.API;

namespace Tasko.Server.Services;

internal static class ApplicationService
{

    internal static void RegisterApplication(this WebApplication application)
    {
        if (application.Environment.IsDevelopment())
        {
            application.UseSwagger();
            application.UseSwaggerUI();
        }

        application.UseHttpsRedirection();

        application.MapGet("/api/v1/users", UserService.GetUsersList());

        application.MapGet("/", () =>
        {
            return "Hello, It's Tasko";
        });
    }

}

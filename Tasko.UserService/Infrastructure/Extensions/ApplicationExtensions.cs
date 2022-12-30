using Tasko.General.Extensions;
using Tasko.General.Models;
using Tasko.Service.Infrasructure.Middlewares;

namespace Tasko.Service.Infrastructure.Extensions;

internal static class ApplicationExtensions
{

    internal static void RegisterApplication(this WebApplication app)
    {

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<JwtMiddleware>();

        app.Services.GetServices<IRouteHandler>()
                    .ToList()
                    .ForEach(api => api.Register(app));
    }

    internal static void RegisterBuilder(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssemblyContaining<UpdateUserValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();

        builder.Services.AddTransient<JwtValidationParameter>();

        builder.Services.AddTransient<IRouteHandler, Api.RouteHandler>();

        builder.Services.AddScoped<IUserRepository, UserRepository>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAuthorization();
        builder.Services.AddAuthorization();
        builder.Services.RegisterJwtBearerAuth(builder);
        builder.Services.RegisterSwaggerAuthorization();

        builder.RegisterMongoDataBase();
    }

}
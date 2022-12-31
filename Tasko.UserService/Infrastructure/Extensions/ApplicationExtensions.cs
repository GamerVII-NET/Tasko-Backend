using Tasko.Service.Infrastructure.RouteHandlers;
using Tasko.Validation.Validators;
using Tasko.Mongo.Extensions;
using Tasko.Configuration.Extensions;

namespace Tasko.Service.Infrastructure.Extensions;

internal static class ApplicationExtensions
{
    //TODO: Add logging
    internal static void RegisterBuilder(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssemblyContaining<UpdateUserValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
        builder.AddMongoDbContext();
        builder.Services.AddTransient<ValidationParameter>();
        builder.Services.AddTransient<IRouteHandler<WebApplication>, UserRouteHandler>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAuthorization();
        builder.Services.AddAuthorization();
        builder.Services.AddJwtBearerAuth(builder);
        builder.Services.AddSwaggerJwtAuthorization();
    }

    internal static void RegisterApplication(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseAuthorization();
        app.UseAuthentication();
        //app.UseMiddleware<JwtMiddleware>(); ????
        app.UseRouteHandlers();
    }


}
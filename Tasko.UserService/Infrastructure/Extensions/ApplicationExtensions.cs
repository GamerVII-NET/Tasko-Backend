using Tasko.Service.Infrastructure.RouteHandlers;
using Tasko.Validation.Validators;
using Tasko.Configuration.Extensions;
using Tasko.Logger.Extensions;
using ILogger = NLog.ILogger;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Tasko.Service.Infrastructure.Extensions;

internal static class ApplicationExtensions
{
    internal static void RegisterBuilder(this WebApplicationBuilder builder)
    {

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddHttpContextAccessor();
         
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        #region Logger 
        //builder.Logging.ClearProviders();
        //builder.Host.UseNLog();
        #endregion

        #region Validator
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssemblyContaining<UpdateUserValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
        #endregion

        #region Database
        builder.AddMongoDbContext();
        #endregion

        #region JWT
        builder.Services.AddSingleton<ValidationParameter>();
        builder.Services.AddJwtBearerAuth(builder);
        #endregion

        #region Swagger
        builder.Services.AddSwaggerGen();
        builder.Services.AddSwaggerJwtAuthorization();
        #endregion

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IRouteHandler<WebApplication>, UserRouteHandler>();
        builder.Services.AddAuthorization(opitions =>
        {
            opitions.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build();
        });
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
    }

    internal static void RegisterApplication(this WebApplication app, ILogger logger)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseAuthorization();
        app.UseAuthentication();
        //app.UseMiddleware<JwtMiddleware>(); ????
        app.UseExceptionHandlerMiddleware(logger);
        app.UseRouteHandlers();
    }
}
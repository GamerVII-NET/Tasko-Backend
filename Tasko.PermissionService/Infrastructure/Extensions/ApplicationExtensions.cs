using Microsoft.AspNetCore.Authentication.JwtBearer;
using Tasko.Configuration.Extensions;
using Tasko.Logger.Extensions;
using Tasko.Service.MiddleWares;
using Tasko.Validation.Validators;
using ILogger = NLog.ILogger;

namespace Tasko.Service.Infrastructure.Extensions
{
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

            builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
            builder.Services.AddTransient<IRouteHandler<WebApplication>, RouteHandlers.RouteHandler>();
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
            app.UseMiddleware<JwtMiddleware>();
            app.UseExceptionHandlerMiddleware(logger);
            app.UseRouteHandlers();
        }
    }
}
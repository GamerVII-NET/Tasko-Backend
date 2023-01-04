using Tasko.Configuration.Extensions;
using Tasko.Domains.Interfaces;
using Tasko.Jwt.Models;
using Tasko.Logger.Extensions;
using Tasko.MicroService.Infrastructure.Repositories;
using Tasko.MicroService.Infrastructure.RouteHandlers;
using Tasko.Validation.Validators;
using ILogger = NLog.ILogger;


namespace Tasko.MicroService.Infrastructure.Extensions
{
    internal static class ApplicationConfiguration
    {
        internal static void RegisterBuilder(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            #region Validator
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddFluentValidation(validation => validation.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
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
            builder.Services.AddSwaggerJwtAuthorization();
            builder.Services.AddSwaggerGen();
            #endregion

            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddTransient<IRouteHandler<WebApplication>, AuthRouteHandler>();
            builder.Services.AddAuthorization();
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
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseExceptionHandlerMiddleware(logger);
            app.UseRouteHandlers();
        }
    }
}
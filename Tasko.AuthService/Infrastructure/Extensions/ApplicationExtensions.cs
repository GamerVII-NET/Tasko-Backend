using Tasko.Configuration.Extensions;
using Tasko.Domains.Interfaces;
using Tasko.Jwt.Models;
using Tasko.Logger.Extensions;
using Tasko.Service.Infrasructure.Middlewares;
using Tasko.Validation.Validators;

namespace Tasko.Service.Infrastructure.Extensions
{
    internal static class ApplicationExtensions
    {

        internal static void RegisterBuilder(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            //builder.Logging.ClearProviders();
            //builder.Host.UseNLog();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateUserValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();

            builder.AddMongoDbContext();

            builder.Services.AddSingleton<ValidationParameter>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            //builder.Services.AddTransient<IRouteHandler<WebApplication>, UserRouteHandler>();

            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthorization();
            builder.Services.AddAuthorization();
            builder.Services.AddJwtBearerAuth(builder);
            builder.Services.AddSwaggerJwtAuthorization();
        }

        internal static void RegisterApplication(this WebApplication app, ILogger logger = null)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthorization();
            app.UseAuthentication();
            //app.UseMiddleware<JwtMiddleware>(); ????
            //app.UseExceptionHandlerMiddleware(logger);
            app.UseRouteHandlers();
            app.UseStatusCodePages();
        }

    }
}
using Tasko.General.Validations;
using Tasko.PermissionService.Infrastructure.Api;
using Tasko.PermissionService.Infrastructure.Repositories;

namespace Tasko.PermissionService.Infrastructure.Configurations
{
    internal static class ApplicationConfiguration
    {
        internal static void RegisterBuilder(this WebApplicationBuilder builder, IMongoDatabase dataContext)
        {

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<PermissionValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<PermissionUpdateValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<PermissionCreateValidator>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options => Jwt.GenerateConfig(ref options, builder.Configuration.GetJwtValidationParameter()));

            builder.Services.AddSingleton(dataContext);
            builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
            builder.Services.AddTransient<General.Interfaces.IRouteHandler, PermissionApi>();

            builder.Services.AddSwaggerGen(s =>
            {
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Insert JWT token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[]{}
                }
            });
            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
        }
        internal static void RegisterApplication(this WebApplication application)
        {
            if (application.Environment.IsDevelopment())
            {
                application.UseSwagger();
                application.UseSwaggerUI();
            }
            application.UseAuthentication();
            application.UseAuthorization();
        }
    }
}

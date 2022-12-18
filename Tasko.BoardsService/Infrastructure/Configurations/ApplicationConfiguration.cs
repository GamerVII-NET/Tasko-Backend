
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Tasko.General.Commands;
using Tasko.General.Extensions.Jwt;
using Tasko.General.Interfaces;
using Tasko.General.Validations;
using Tasko.UserRoles.Infrasructure.Repositories;

namespace Tasko.UserService.Infrasructure.Configurations
{
    internal static class ApplicationConfiguration
    {
        internal static void RegisterBuilder(this WebApplicationBuilder builder, IMongoDatabase dataContext)
        {
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateUserValidator>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options => Jwt.GenerateConfig(ref options, builder.Configuration.GetJwtValidationParameter()));

            builder.Services.AddSingleton(dataContext);
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddTransient<IApi, UserApi>();

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

            //if (application.Environment.IsProduction())
            //{
            //    application.Urls.Add("http://87.249.49.56:7177");
            //}

            //application.UseHttpsRedirection();
            application.UseAuthentication();
            application.UseAuthorization();
        }
    }
}
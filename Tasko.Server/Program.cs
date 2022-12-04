
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Tasko.Server.Context.Data.Context;
using Tasko.Server.Repositories.UserRepository;
using Tasko.Server.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var deployConnectionString = builder.Configuration.GetConnectionString("DeployConnection");

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => AddJwtBearer
    .GenerateConfig(options, builder));

builder.Services.AddDbContext<DataBaseContext>(options =>options.UseNpgsql(deployConnectionString));
var application = builder.Build();

application.RegisterApplication(builder);

application.Run();
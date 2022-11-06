
using Microsoft.EntityFrameworkCore;
using Tasko.Server.Context.Data.Context;
using Tasko.Server.Repositories.UserRepository;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataBaseContext>(options =>options.UseSqlServer(connectionString));
var application = builder.Build();

application.RegisterApplication();

application.Run();
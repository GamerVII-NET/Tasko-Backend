using Tasko.Server.Infrastructure.API.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var mongoConfig = "ConnectionStrings:Mongo";
var databaseContext = ApplicationService.GetMongoDataConext(config[$"{mongoConfig}:Connection"], config[$"{mongoConfig}:Database"]);
builder.RegisterBuilder(databaseContext);
var application = builder.Build();
application.RegisterApplication(builder);
application.Services.GetServices<IApi>()
                    .ToList()
                    .ForEach(api => api.Register(application));
application.Run();
using Tasko.AuthService.Infrastructure.Configurations;
using Tasko.General.Commands;
using Tasko.General.Extensions;
using Tasko.General.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.SetSettingFile(@"../../Tasko-Backend/Tasko.General/", "appsettings.json");
var dbConnectionString = builder.Configuration.GetMongoConnectionString();
var dbName = builder.Configuration.GetMongoDatabaseName();
var databaseContext = Mongo.GetMongoDataConext(dbConnectionString, dbName);
builder.RegisterBuilder(databaseContext);
var application = builder.Build();
application.RegisterApplication(builder);
application.Services.GetServices<IApi>()
                    .ToList()
                    .ForEach(api => api.Register(application));
application.Run();
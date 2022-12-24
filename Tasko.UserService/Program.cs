using Tasko.UserService.Infrasructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

#region If project on local machine
//builder.SetSettingFile(@"../../Tasko-Backend/Tasko.General/", "appsettings.json");
#endregion
#region If project on docker container
builder.SetSettingFile(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "appsettings.json");//If project on docker
#endregion

var dbConnectionString = builder.Configuration.GetMongoConnectionString();
var dbName = builder.Configuration.GetMongoDatabaseName();
var databaseContext = Mongo.GetMongoDataConext(dbConnectionString, dbName);

builder.RegisterBuilder(databaseContext);

var application = builder.Build();
application.RegisterApplication();

application.Services.GetServices<IApi>()
                    .ToList()
                    .ForEach(api => api.Register(application));
application.Run();
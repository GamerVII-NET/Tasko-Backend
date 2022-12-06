using Tasko.Server.Infrastructure.API.Interfaces;
using Tasko.Server.Infrastructure.Extensions.AES;
using Tasko.Server.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
var key = AesService.AesKey;
var IV = AesService.IV;
var dbConnectionString = builder.Configuration[$"ConnectionStrings:Mongo:Connection"].Decrypt(key, IV);
var dbName = builder.Configuration[$"ConnectionStrings:Mongo:Database"].Decrypt(key, IV);
var databaseContext = ApplicationService.GetMongoDataConext(dbConnectionString, dbName);
builder.RegisterBuilder(databaseContext);
var application = builder.Build();
application.RegisterApplication(builder);
application.Services.GetServices<IApi>()
                    .ToList()
                    .ForEach(api => api.Register(application));
application.Run();
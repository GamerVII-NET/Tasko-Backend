using Tasko.Server.Infrastructure.API.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var databaseContext = ApplicationService.GetMongoDataConext(builder.Configuration["ConnectionStrings:Mongo:Connection"], 
                                                            builder.Configuration["ConnectionStrings:Mongo:Database"]);
builder.RegisterBuilder(databaseContext);
var application = builder.Build();
application.RegisterApplication(builder);
application.Services.GetServices<IApi>().ToList().ForEach(api => api.Register(application));
application.Run();
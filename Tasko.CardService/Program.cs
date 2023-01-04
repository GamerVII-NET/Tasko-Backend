
using Tasko.CardService.Infrastructure.Repositories;
using Tasko.CardService.Infrastructure.Services;

var builer = WebApplication.CreateBuilder(args);
builer.Services.AddTransient<ICardRepository, CardRepository>();
builer.Services.AddGrpc();
builer.Services.AddGrpcReflection();
var app = builer.Build();
app.MapGrpcService<CardService>();
app.MapGrpcReflectionService();
app.Run();
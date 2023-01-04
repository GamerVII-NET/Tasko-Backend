
using Tasko.CardService.Infrastructure.Repositories;
using Tasko.CardService.Infrastructure.Services;

var builer = WebApplication.CreateBuilder(args);
builer.Services.AddGrpc();
builer.Services.AddGrpcReflection();
builer.Services.AddTransient<ICardRepository, CardRepository>();
var app = builer.Build();
app.MapGrpcService<CardService>();
app.MapGrpcReflectionService();
app.MapGet("/",() => "Test");
app.Run();
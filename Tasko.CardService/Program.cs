var builer = WebApplication.CreateBuilder(args);
builer.Services.AddGrpc();
var app = builer.Build();
app.Run();
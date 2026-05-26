using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(o => o.AddServerHeader = false);

builder.AddServiceDefaults();
builder.AddApplicationServices();

var app = builder.Build();

app.AddApplicationMiddlewares();

app.Run();

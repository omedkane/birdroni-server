using Birdroni.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<BirdroniDatabaseSettings>(
    builder.Configuration.GetSection("BirdroniDatabase")
);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

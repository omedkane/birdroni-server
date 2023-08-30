using Birdroni.Models;
using Birdroni.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<BirdroniDatabaseSettings>(
    builder.Configuration.GetSection("BirdroniDatabase")
);

builder.Services.AddSingleton<UsersService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

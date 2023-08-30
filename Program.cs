using Birdroni.Models;
using Birdroni.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<BirdroniDatabaseSettings>(
    builder.Configuration.GetSection("BirdroniDatabase")
);

builder.Services.AddSingleton<UsersService>();

var app = builder.Build();
app.MapGet("/", () => "hello world");
app.MapControllers();

app.Run();

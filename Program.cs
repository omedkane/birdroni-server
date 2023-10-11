using System.Text;
using Birdroni.Contexts;
using Birdroni.Misc.Security;
using Birdroni.Models;
using Birdroni.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<BirdroniDatabaseSettings>(
    builder.Configuration.GetSection("Databases:BirdroniDatabase")
);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
        options =>
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["JWT:Issuer"],
                ValidAudience = builder.Configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]!)
                )
            }
    );

builder.Services.AddSingleton<LocalDBContext>();
builder.Services.AddSingleton<UsersService>();
builder.Services.AddSingleton<JWToken>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "hello world");
app.MapControllers();

app.Run();

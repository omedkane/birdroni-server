using Birdroni.Models;
using Microsoft.EntityFrameworkCore;

namespace Birdroni.Contexts;

public class LocalDBContext : DbContext
{
    public DbSet<BlacklistedToken> BlacklistedTokens { get; set; } = null!;
    public string DbPath { get; }

    public LocalDBContext(IConfiguration configuration)
    {
        DbPath = string.Format(
            configuration["Databases:LocalDatabase:Path"]
                ?? throw new Exception("Local database path not available"),
            Path.DirectorySeparatorChar
        );

    }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseSqlite($"DataSource={DbPath}");
}

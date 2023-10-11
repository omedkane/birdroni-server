namespace Birdroni.Models;

public class BlacklistedToken
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Token { get; set; }
    public required DateTime ExpirationDate { get; set; }
}

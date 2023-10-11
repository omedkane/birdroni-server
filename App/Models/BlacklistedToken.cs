namespace Birdroni.Models;

public class BlacklistedToken
{
    public required string Id { get; set; }
    public required string Token { get; set; }
    public required DateTime ExpirationDate { get; set; }
}

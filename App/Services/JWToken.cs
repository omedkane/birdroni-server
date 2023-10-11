using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Birdroni.Misc.Security;

public class JWTContext
{
    public string SecretKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
}

public class JWToken
{
    public JwtSecurityTokenHandler tokenHander = new();

    private readonly JWTContext Context = new();

    public JWToken(IConfiguration config)
    {
        config.GetSection("JWT").Bind(Context);
    }

    public string GenerateToken(string userId)
    {
        byte[] key = Encoding.UTF8.GetBytes(Context.SecretKey);
        SecurityTokenDescriptor tokenDescriptor =
            new()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new(ClaimTypes.PrimarySid, userId),
                        new(ClaimTypes.Role, "user"),
                        new("scope", "databases")
                    }
                ),
                IssuedAt = DateTime.Now,
                Issuer = Context.Issuer,
                Audience = Context.Audience,
                Expires = DateTime.Now.AddDays(3),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
            };

        return tokenHander.CreateEncodedJwt(tokenDescriptor);
    }

    public JwtSecurityToken GetJWT(string token) => tokenHander.ReadJwtToken(token);
}

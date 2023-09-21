using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

public static class PwdHasher
{
    public static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
        string hashed = Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
            )
        );
        return hashed;
    }

    public static bool Match(string providedPassword, string hashedPassword)
    {
        string hashedProvidedPassword = PwdHasher.HashPassword(providedPassword);
        return hashedProvidedPassword.Equals(hashedPassword);
    }
}

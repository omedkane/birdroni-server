namespace Birdroni.DTOs;

public class RegisterDTO
{
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Avatar { get; set; } = null!;
}

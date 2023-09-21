using System.ComponentModel.DataAnnotations;

namespace Birdroni.Resources;

public record LoginResource
{
    [Required(ErrorMessage = "Email requis !")]
	[EmailAddress(ErrorMessage = "Addresse electronique incorrecte !")]
    public required string email { get; set; }
    
    [Required(ErrorMessage = "Please enter a valid password !")]
    [MinLength(8, ErrorMessage = "Your password must at least be of 8 caracters")]
    public required string password { get; set; }
};

using System.ComponentModel.DataAnnotations;

namespace Birdroni.Resources;

public record RegisterResource(
    [Required(ErrorMessage = "Please enter your first name !")]
    string Firstname,
    
    [Required(ErrorMessage = "Please enter your last name !")]
    string Lastname,
    
    [Required(ErrorMessage = "Please enter your email address !")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address !")]
    string Email,
    
    [Required(ErrorMessage = "Please enter a valid password !")]
    [MinLength(8, ErrorMessage = "Your password must at least be of 8 caracters")]
    string Password,
    
    [Url(ErrorMessage ="You must provide a valid URL for your profile picture")]
    string Avatar
);

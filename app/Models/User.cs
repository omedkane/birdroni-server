using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Birdroni.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid Id { get; set; } = Guid.Empty;

    [DataType(DataType.EmailAddress)]
    [Required]
    public string Email { get; set; } = null!;

    [StringLength(50, MinimumLength = 2)]
    [Required]
    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Avatar { get; set; } = null!;

    public User() { }
}

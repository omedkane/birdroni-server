using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Birdroni.Models;

public sealed class User
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid Id { get; set; } = Guid.Empty;

    public required string Email { get; set; }

    public required string Firstname { get; set; }

    public required string Lastname { get; set; }

    public string? Avatar { get; set; }

    public required string HashedPassword { get; set; }
    public required byte[] Salt { get; set; }

    public User() { }
}

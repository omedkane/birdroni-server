using Birdroni.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Birdroni.Services;

public class UsersService
{
    private readonly IMongoCollection<User> _usersCollection;

    public UsersService(IOptions<BirdroniDatabaseSettings> dbSettings)
    {
        var client = new MongoClient(dbSettings.Value.ConnectionString);
        var database = client.GetDatabase(dbSettings.Value.DatabaseName);
        _usersCollection = database.GetCollection<User>("users");
    }
}

using Birdroni.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
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

    public async Task<List<User>> GetAllAsync() =>
        await _usersCollection.Find(_ => true).ToListAsync();

    public async Task CreateAsync(User user) => await _usersCollection.InsertOneAsync(user);

    public async Task<User> GetUserAsync(string email) =>
        (await _usersCollection.FindAsync(user => user.Email == email)).SingleOrDefault();

    public async Task<User> GetUserAsync(string email, string hashedPassword) =>
        (
            await _usersCollection.FindAsync(
                user => user.Email == email && user.HashedPassword == hashedPassword
            )
        ).SingleOrDefault();
}

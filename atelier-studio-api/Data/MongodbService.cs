using MongoDB.Driver;
using atelier_studio_api.Entities;

namespace atelier_studio_api.Data;

public class MongodbService
{
    private readonly IConfiguration _configuration;
    private readonly IMongoDatabase? _database;

    public MongodbService(IConfiguration configuration)
    {
        _configuration = configuration;

        var connectionString = _configuration.GetConnectionString("Mongodb");
        var mongoUrl = MongoUrl.Create(connectionString);
        var mongoCLient = new MongoClient(mongoUrl);
        _database = mongoCLient.GetDatabase(mongoUrl.DatabaseName);
    }
    
    public IMongoDatabase? Database => _database;
    
    public async Task CreateIndexes()
    {
        var reviewCollection = _database.GetCollection<Review>("Review");

        var reviewTextIndex = new CreateIndexModel<Review>(
            Builders<Review>.IndexKeys.Ascending(r => r.ReviewText),
            new CreateIndexOptions { Unique = true }
        );

        await reviewCollection.Indexes.CreateOneAsync(reviewTextIndex);
    }
}
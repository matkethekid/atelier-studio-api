using MongoDB.Driver;

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
}
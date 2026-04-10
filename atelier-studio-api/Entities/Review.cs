using MongoDB.Bson.Serialization.Attributes;

namespace atelier_studio_api.Entities;

public class Review
{
    [BsonId]
    public string Id { get; set; }
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("review")]
    public string ReviewText { get; set; }
}
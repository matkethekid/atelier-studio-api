using MongoDB.Bson.Serialization.Attributes;

namespace atelier_studio_api.Entities;

public class ReviewLink
{
    [BsonId]
    public string Id { get; set; }
    [BsonElement("link")]
    public Guid Link { get; set; }
    [BsonElement("expired")]
    public bool Expired { get; set; }
}
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace atelier_studio_api.Entities;

public class Teacher
{
    [BsonId]
    public string? Id { get; set; }
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("location")]
    public string Location { get; set; }
    [BsonElement("languages")]
    public List<string> Languages { get; set; }
    [BsonElement("profilepictureurl")]
    public string ProfilePictureUrl { get; set; }
}
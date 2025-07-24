using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Registration_Test.Entities;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)] // MongoDB için _id alanı
    public string MongoId { get; set; }

    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}
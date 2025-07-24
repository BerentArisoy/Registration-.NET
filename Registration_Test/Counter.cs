using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Registration_Test;

public class Counter
{
    [BsonId]
    public string Id { get; set; }
    public int Sequence { get; set; }
}
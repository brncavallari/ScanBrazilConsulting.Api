using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities.MongoDb.v1.WorkTimer;
public class WorkTimerInformation
{
    [BsonElement("name")]
    public string Name { get; set; }
}

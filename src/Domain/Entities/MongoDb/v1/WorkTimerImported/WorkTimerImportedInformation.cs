using MongoDB.Bson;

namespace Domain.Entities.MongoDb.v1.WorkTimerImported;

public class WorkTimerImportedInformation
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("fileName")]
    public string FileName { get; set; }

    [BsonElement("year")]
    public string Year { get; set; }

    [BsonElement("month")]
    public string Month { get; set; }
}
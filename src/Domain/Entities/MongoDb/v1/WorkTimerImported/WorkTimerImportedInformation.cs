namespace Domain.Entities.MongoDb.v1.WorkTimerImported;

public class WorkTimerImportedInformation
{
    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("fileName")]
    public string FileName { get; set; }

    [BsonElement("year")]
    public string Year { get; set; }

    [BsonElement("month")]
    public string Month { get; set; }
}
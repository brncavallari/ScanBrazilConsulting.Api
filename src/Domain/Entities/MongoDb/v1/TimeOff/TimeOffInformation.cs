using MongoDB.Bson;

namespace Domain.Entities.MongoDb.v1.TimeOff;
public sealed class TimeOffInformation
{
    [BsonElement("id")]
    public ObjectId Id { get; set; }
    [BsonElement("startDate")]
    public DateTime StartDate { get; set; }
    [BsonElement("endDate")]
    public DateTime EndDate { get; set; }
    [BsonElement("userEmail")]
    public string UserEmail { get; set; }
    [BsonElement("hour")]
    public double Hour { get; set; }
}

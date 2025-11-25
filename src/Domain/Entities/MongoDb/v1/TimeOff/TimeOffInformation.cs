using Domain.Enum.v1;
using MongoDB.Bson;

namespace Domain.Entities.MongoDb.v1.TimeOff;
public sealed class TimeOffInformation
{
    [BsonElement("protocol")]
    public string Protocol { get; set; }
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
    [BsonElement("status")]
    public TimeOffEnum Status { get; set; }
    [BsonElement("approver")]
    public string Approver { get; set; }
    [BsonElement("remark")]
    public string Remark { get; set; }
    [BsonElement("description")]
    public string Description { get; set; }
    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }
}

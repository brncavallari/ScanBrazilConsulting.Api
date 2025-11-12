namespace Domain.Entities.MongoDb.v1.UserTimer;
public class UserTimerInformation
{
    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("hour")]
    public double Hour { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("updateAt")]
    public DateTime UpdateAt { get; set; }
}
namespace Domain.Entities.MongoDb.v1.UserTimer;
public sealed class UserTimerInformation
{
    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("hour")]
    public double Hour { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("remark")]
    public IList<Remark> Remarks { get; set; }

    public void SetUserTimer(
        double hour,
        string description,
        string userName)
    {
        Hour += hour;
        Remark remark = new() { Value = hour, Description = description, UpdateAt = DateTime.UtcNow, UserName = userName };

        if (Remarks is null)
            Remarks = [remark];
        else
            Remarks.Add(remark);
    }
}

public sealed class Remark
{
    [BsonElement("description")]
    public string Description { get; set; }
    [BsonElement("value")]
    public double Value { get; set; }
    [BsonElement("userName")]
    public string UserName { get; set; }
    [BsonElement("updateAt")]
    public DateTime UpdateAt { get; set; }
}

using Domain.Entities.MongoDb.v1.UserTimer;

namespace Infrastructure.Data.Query.Queries.v1.UserTimer.GetAllUserTimer;

public sealed class GetAllUserTimerQueryResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public double Hour { get; set; }
    public IEnumerable<Remark> Remarks { get; set; }

    public static implicit operator GetAllUserTimerQueryResponse(UserTimerInformation userTimerInformation)
    {
        if (userTimerInformation is null) return new GetAllUserTimerQueryResponse();

        return new()
        {
            Id = userTimerInformation.Id.ToString(),
            Name = userTimerInformation.Name,
            Email = userTimerInformation.Email,
            Hour = userTimerInformation.Hour,
            Remarks = userTimerInformation.Remarks?.Select(r => new Remark
            {
                Description = r.Description,
                Value = r.Value,
                UpdateAt = r.UpdateAt,
                UserName = r.UserName
            })
        };
    }
}

public sealed class Remark
{
    public string Description { get; set; }
    public string UserName { get; set; }
    public double Value { get; set; }
    public DateTime UpdateAt { get; set; }
}
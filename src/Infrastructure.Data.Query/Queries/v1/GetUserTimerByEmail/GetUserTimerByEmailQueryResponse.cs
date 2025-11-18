using Domain.Entities.MongoDb.v1.UserTimer;

namespace Infrastructure.Data.Query.Queries.v1.GetUserTimerByEmail;
public class GetUserTimerByEmailQueryResponse
{
    public string Name { get; set; }
    public string Email { get; set; }
    public double Hour { get; set; }
    public DateTime UpdateAt { get; set; }

    public static implicit operator GetUserTimerByEmailQueryResponse(UserTimerInformation userTimerInformation)
    {
        if (userTimerInformation is null) return new GetUserTimerByEmailQueryResponse();

        return new()
        {
            Name = userTimerInformation.Name,
            Email = userTimerInformation.Email,
            Hour = userTimerInformation.Hour
        };
    }
}

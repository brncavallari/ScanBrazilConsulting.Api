using MongoDB.Bson;

namespace Domain.Commands.v1.UserTimer.Create;
public sealed class CreateUserTimerCommand : IRequest
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string EmailAlternative { get; set; }
    public double Hour { get; set; }
    public string Remark { get; set; }

    public static implicit operator UserTimerInformation(CreateUserTimerCommand createUserTimerCommand)
    {
        return new()
        {
            Id = createUserTimerCommand.Id is not null ? ObjectId.Parse(createUserTimerCommand.Id) : ObjectId.Empty,
            Hour = createUserTimerCommand.Hour,
            Remarks = [],
            Name = createUserTimerCommand.Name,
            Email = createUserTimerCommand.Email,
            EmailAlternative = createUserTimerCommand.EmailAlternative
        };
    }
}

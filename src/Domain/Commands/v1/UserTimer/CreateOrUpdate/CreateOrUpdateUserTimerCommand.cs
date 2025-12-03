using MongoDB.Bson;

namespace Domain.Commands.v1.UserTimer.CreateOrUpdate;
public sealed class CreateOrUpdateCommand : IRequest
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string EmailAlternative { get; set; }
    public double Hour { get; set; }
    public string Remark { get; set; }

    public static implicit operator UserTimerInformation(CreateOrUpdateCommand createOrUpdateCommand)
    {
        return new()
        {
            Id = !string.IsNullOrEmpty(createOrUpdateCommand.Id) ? ObjectId.Parse(createOrUpdateCommand.Id) : ObjectId.Empty,
            Hour = createOrUpdateCommand.Hour,
            Remarks = [],
            Name = createOrUpdateCommand.Name,
            Email = createOrUpdateCommand.Email,
            EmailAlternative = createOrUpdateCommand.EmailAlternative
        };
    }
}

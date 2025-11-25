using Domain.Entities.MongoDb.v1.TimeOff;

namespace Domain.Commands.v1.TimeOff.Create;
public sealed class CreateTimeOffCommand : IRequest
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double Hour { get; set; }
    public string UserEmail { get; set; }

    public static implicit operator TimeOffInformation(CreateTimeOffCommand createTimeOffCommand)
    {
        return new()
        {
            StartDate = createTimeOffCommand.StartDate,
            EndDate = createTimeOffCommand.EndDate,
            Hour = createTimeOffCommand.Hour,   
            UserEmail = createTimeOffCommand.UserEmail,
        };
    }
}

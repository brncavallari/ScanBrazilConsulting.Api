using Domain.Entities.MongoDb.v1.TimeOff;
using Domain.Enum.v1;
using Domain.Service.v1;

namespace Domain.Commands.v1.TimeOff.Create;
public sealed class CreateTimeOffCommand : IRequest
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double Hour { get; set; }
    public string UserEmail { get; set; }
    public string Remark { get; set; }

    public static implicit operator TimeOffInformation(CreateTimeOffCommand createTimeOffCommand)
    {
        return new()
        {
            Protocol = ProtocolService.GenerateProtocol(createTimeOffCommand.UserEmail),
            StartDate = createTimeOffCommand.StartDate,
            EndDate = createTimeOffCommand.EndDate,
            Hour = createTimeOffCommand.Hour,
            UserEmail = createTimeOffCommand.UserEmail,
            Status = TimeOffEnum.Pending,
            CreatedAt = DateTime.UtcNow,
            Remark = createTimeOffCommand.Remark
        };
    }
}

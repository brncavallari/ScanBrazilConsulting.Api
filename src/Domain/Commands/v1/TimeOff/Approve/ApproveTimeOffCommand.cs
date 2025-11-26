namespace Domain.Commands.v1.TimeOff.Approve;
public sealed class ApproveTimeOffCommand(
    string protocol) : IRequest
{
    public string Protocol { get; set; } = protocol;
    public string Description { get; set; }
}
namespace Domain.Commands.v1.TimeOff.Reject;
public sealed class RejectTimeOffCommand(
    string protocol) : IRequest
{
    public string Protocol { get; set; } = protocol;
    public string Description { get; set; }
}

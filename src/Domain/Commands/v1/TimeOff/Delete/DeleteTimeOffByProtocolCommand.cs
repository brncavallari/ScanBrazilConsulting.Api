namespace Domain.Commands.v1.TimeOff.Delete;
public sealed class DeleteTimeOffByProtocolCommand(
    string protocol) : IRequest
{
    public string Protocol { get; set; } = protocol;
}

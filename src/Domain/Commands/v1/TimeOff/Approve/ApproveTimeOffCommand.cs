namespace Domain.Commands.v1.TimeOff.Approve;
public sealed class ApproveTimeOffCommand : IRequest
{
    public string Protocol { get; set; }
    public string UserEmail { get; set; } 
    public string Description { get; set; }
}
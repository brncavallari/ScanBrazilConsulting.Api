namespace Domain.Commands.v1.UserTimer.Update;
public sealed class UpdateUserTimerCommand : IRequest<Unit>
{
    public string Email { get; set; }
    public double Hour { get; set; }
    public string Remark { get; set; }
}

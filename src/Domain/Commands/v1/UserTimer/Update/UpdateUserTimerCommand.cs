namespace Domain.Commands.v1.UserTimer.Update;

public class UpdateUserTimerCommand : IRequest<Unit>
{
    public string Email { get; set; }

    public double Hour { get; set; }
    public string Comment { get; set; }
}

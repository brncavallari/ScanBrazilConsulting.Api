namespace Domain.Commands.v1.UserTimer.Create;
public class UserTimerCommandHandler(
    IUserTimerRepository _userTimerRepository,
    IWorkTimerRepository _workTimerRepository) : IRequestHandler<UserTimerCommand, Unit>
{

    public async Task<Unit> Handle(UserTimerCommand request, CancellationToken cancellationToken)
    {
        return Unit.Value;
    }
}
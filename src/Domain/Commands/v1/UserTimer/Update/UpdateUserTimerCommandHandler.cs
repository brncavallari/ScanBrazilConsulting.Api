namespace Domain.Commands.v1.UserTimer.Update;

public class UpdateUserTimerCommandHandler(
    IUserTimerRepository _userTimerRepository
    ) : IRequestHandler<UpdateUserTimerCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUserTimerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userTimer = await _userTimerRepository.FindEmailAsync(request.Email);

            if (userTimer is not null)
            {
                userTimer.Hour += request.Hour;
                await _userTimerRepository.UpsertUserTimerAsync(userTimer);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return Unit.Value;
    }
}

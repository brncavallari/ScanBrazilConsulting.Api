namespace Domain.Commands.v1.UserTimer.Update;
public sealed class UpdateUserTimerCommandHandler(
    IUserTimerRepository _userTimerRepository
    ) : IRequestHandler<UpdateUserTimerCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUserTimerCommand updateUserTimerCommand, CancellationToken cancellationToken)
    {
        try
        {
            var userTimer = await _userTimerRepository.FindEmailAsync(updateUserTimerCommand.Email);

            if (userTimer is not null)
            {
                userTimer.SetUserTimer(
                    updateUserTimerCommand.Hour,
                    updateUserTimerCommand.Remark,
                    updateUserTimerCommand.UserName
                );

                await _userTimerRepository.UpsertUserTimerAsync(
                    userTimer);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return Unit.Value;
    }
}

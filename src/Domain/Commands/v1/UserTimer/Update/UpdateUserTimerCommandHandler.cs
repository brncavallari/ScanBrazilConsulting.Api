using Domain.Interfaces.v1.Context;
using Domain.Interfaces.v1.Repositories.UserTimer;

namespace Domain.Commands.v1.UserTimer.Update;
public sealed class UpdateUserTimerCommandHandler(
    IUserTimerRepository _userTimerRepository,
    IUserContext _userContext) : IRequestHandler<UpdateUserTimerCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUserTimerCommand updateUserTimerCommand, CancellationToken cancellationToken)
    {
        try
        {
            var userTimer = await _userTimerRepository.FindByEmailAsync(updateUserTimerCommand.Email);

            if (userTimer is not null)
            {
                userTimer.SetUserTimer(
                    updateUserTimerCommand.Hour,
                    updateUserTimerCommand.Remark,
                    _userContext.UserName ?? string.Empty
                );

                await _userTimerRepository.UpsertAsync(
                    userTimer);
            }
        }
        catch (Exception)
        {
            throw;
        }

        return Unit.Value;
    }
}

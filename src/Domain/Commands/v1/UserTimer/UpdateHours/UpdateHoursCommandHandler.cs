using Domain.Interfaces.v1.Context;
using Domain.Interfaces.v1.Repositories.UserTimer;

namespace Domain.Commands.v1.UserTimer.UpdateHours;
public sealed class UpdateUserTimerCommandHandler(
    IUserTimerRepository _userTimerRepository,
    IUserContext _userContext) : IRequestHandler<UpdateHoursCommand, Unit>
{
    public async Task<Unit> Handle(UpdateHoursCommand updateHoursTimerCommand, CancellationToken cancellationToken)
    {
        try
        {
            var userTimer = await _userTimerRepository.FindByEmailAsync(updateHoursTimerCommand.Email);

            if (userTimer is not null)
            {
                userTimer.SetRemark(
                    updateHoursTimerCommand.Hour,
                    updateHoursTimerCommand.Remark,
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

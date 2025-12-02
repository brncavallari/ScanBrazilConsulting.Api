using Domain.Interfaces.v1.Repositories.UserTimer;
using MongoDB.Bson;

namespace Domain.Commands.v1.UserTimer.Create;
public sealed class CreateUserTimerCommandHandler(
    IUserTimerRepository _userTimerRepository) : IRequestHandler<CreateUserTimerCommand>
{
    public async Task Handle(CreateUserTimerCommand createUserTimerCommand, CancellationToken cancellationToken)
    {
        try
        {
            UserTimerInformation userTimerInformation = createUserTimerCommand;
            if (userTimerInformation.Id != ObjectId.Empty && !string.IsNullOrEmpty(createUserTimerCommand.Remark))
            {
                var userTimer = await _userTimerRepository.FindByIdAsync(userTimerInformation.Id);

                WorkTimersBuilder.SetRamark(userTimer, createUserTimerCommand.Hour, createUserTimerCommand.Remark, createUserTimerCommand.Name);
                userTimerInformation.Remarks = userTimer.Remarks;
            }
            else if (!string.IsNullOrEmpty(createUserTimerCommand.Remark))
            {
                userTimerInformation.Remarks = WorkTimersBuilder.CreateRemark(createUserTimerCommand.Remark, createUserTimerCommand.Hour, createUserTimerCommand.Name);
            }

            var userTimerInfo = _userTimerRepository.CreateOrUpdateAsync(userTimerInformation);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao criar ou atualizar o temporizador do usuário.", ex);
        }
    }
}

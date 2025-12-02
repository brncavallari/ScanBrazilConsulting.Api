using Domain.Interfaces.v1.Repositories.UserTimer;
using MongoDB.Bson;

namespace Domain.Commands.v1.UserTimer.Delete;
public sealed class DeleteUserTimerCommandHandler(
    IUserTimerRepository _userTimerRepository) : IRequestHandler<DeleteCommand>
{
    public async Task Handle(DeleteCommand deleteCommand, CancellationToken cancellationToken)
    {
        try
        {
            var userInformation = await _userTimerRepository.FindByIdAsync(ObjectId.Parse(deleteCommand.Id));
            if (userInformation is not null)
                await _userTimerRepository.DeleteByIdAsync(userInformation.Id);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao Deletar o usuário.", ex);
        }
    }
}

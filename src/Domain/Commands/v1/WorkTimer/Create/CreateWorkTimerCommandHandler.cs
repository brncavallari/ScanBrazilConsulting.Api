using Domain.Entities.MongoDb.v1.WorkTimer;
using Domain.Interfaces.v1.WorkTimer;
using MediatR;

namespace Domain.Commands.v1.WorkTimer.Create;
internal class CreateWorkTimerCommandHandler(
    IWorkTimerRepository _workTimerRepository) : IRequestHandler<CreateWorkTimerCommand>
{
    public async Task Handle(CreateWorkTimerCommand createWorkTimerCommand, CancellationToken cancellationToken)
    {
        try
        {
            WorkTimerInformation workInformation = createWorkTimerCommand;

            await _workTimerRepository.CreateTesteAsync(
                workInformation
            );
        }
        catch (Exception)
        {
            throw;
        }
    }
}

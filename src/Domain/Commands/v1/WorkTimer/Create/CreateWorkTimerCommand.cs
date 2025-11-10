using Domain.Entities.MongoDb.v1.WorkTimer;
using MediatR;

namespace Domain.Commands.v1.WorkTimer.Create;
public sealed class CreateWorkTimerCommand : IRequest
{
    public string Name { get; set; }

    public static implicit operator WorkTimerInformation(CreateWorkTimerCommand createWorkTimer)
    {
        return new()
        {
            Name = createWorkTimer.Name,
        };
    }
}

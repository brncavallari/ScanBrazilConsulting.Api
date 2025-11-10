using Domain.Entities.MongoDb.v1.WorkTimer;

namespace Domain.Interfaces.v1.WorkTimer;
public interface IWorkTimerRepository
{
    Task CreateTesteAsync(WorkTimerInformation workInformation);
}

namespace Domain.Interfaces.v1.WorkTimer;
public interface IWorkTimerRepository
{
    Task AddAsync(WorkTimerInformation workInformation);
    Task<List<WorkTimerInformation>> GetByFileNameAsync(string fileName);
    Task<bool> ExistTaskAsync(string id);
}

namespace Domain.Interfaces.v1.Repositories.WorkTimer;
public interface IWorkTimerRepository
{
    Task AddAsync(WorkTimerInformation workInformation);
    Task<List<WorkTimerInformation>> GetByFileNameAsync(string fileName);
    Task<bool> ExistTaskAsync(string id);
    Task<bool> DeleteAllTaskByFileNameAsync(string fileName);
}

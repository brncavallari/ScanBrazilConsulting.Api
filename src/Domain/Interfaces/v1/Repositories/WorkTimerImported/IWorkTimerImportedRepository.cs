namespace Domain.Interfaces.v1.Repositories.WorkTimerImported;

public interface IWorkTimerImportedRepository
{
    Task<bool> InsertIfNotExistsAsync(WorkTimerImportedInformation information);
    Task<IReadOnlyList<WorkTimerImportedInformation>> FindAllWorkTimersImportedAsync();
    Task<WorkTimerImportedInformation> FindByIdAsync(string id);
    Task<bool> DeleteTaskByFileNameAsync(string fileName);
}
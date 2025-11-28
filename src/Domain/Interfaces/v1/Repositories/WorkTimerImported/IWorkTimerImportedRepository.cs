namespace Domain.Interfaces.v1.Repositories.WorkTimerImported;

public interface IWorkTimerImportedRepository
{
    Task<bool> InsertIfNotExistsAsync(WorkTimerImportedInformation information);
    Task<IReadOnlyList<WorkTimerImportedInformation>> FindAllAsync();
    Task<WorkTimerImportedInformation> FindByIdAsync(string id);
    Task<bool> DeleteTaskByFileNameAsync(string fileName);
}
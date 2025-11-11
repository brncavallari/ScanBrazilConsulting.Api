namespace Domain.Interfaces.v1.WorkTimerImported;

public interface IWorkTimerImportedRepository
{
    Task<bool> InsertIfNotExistsAsync(WorkTimerImportedInformation information);
}
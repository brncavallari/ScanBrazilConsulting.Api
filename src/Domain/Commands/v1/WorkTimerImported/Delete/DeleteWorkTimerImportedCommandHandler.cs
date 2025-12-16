using Domain.Interfaces.v1.Repositories.UserTimer;
using Domain.Interfaces.v1.Repositories.WorkTimer;
using Domain.Interfaces.v1.Repositories.WorkTimerImported;

namespace Domain.Commands.v1.WorkTimerImported.Delete;
public sealed class DeleteWorkTimerImportedCommandHandler(
        IWorkTimerImportedRepository _workTimerImportedRepository,
        IWorkTimerRepository _workTimerRepository,
        IUserTimerRepository _userTimerRepository
    ) : IRequestHandler<DeleteWorkTimerImportedCommand, Unit>
{
    public async Task<Unit> Handle(DeleteWorkTimerImportedCommand deleteWorkCommand, CancellationToken cancellationToken)
    {
        try
        {
            var importedFile = await _workTimerImportedRepository.FindByIdAsync(deleteWorkCommand.Id) ??
                throw new FileNotFoundException($"File with ID {deleteWorkCommand.Id} not found.");

            var workTimers = await _workTimerRepository.FindByFileNameAsync(importedFile.FileName.Replace(".xlsx", string.Empty));
            if (workTimers is null) return Unit.Value;

            var users = workTimers
                .Where(x => !string.IsNullOrEmpty(x.AssignedTo))
                .GroupBy(x => x.AssignedTo)
                .Select(g => new
                {
                    g.Key,
                    TotalCompletedWork = g.Sum(x => double.TryParse(x.CompletedWork, out double value) ? value : 0)
                })
                .OrderBy(x => x.Key)
                .ToList();

            foreach (var user in users)
            {
                var (name, emailAlternative) = WorkTimersBuilder.ExtractNameAndEmail(user.Key);

                if (string.IsNullOrEmpty(emailAlternative)) continue;

                var existingUser = await _userTimerRepository.FindByEmailAlternativeAsync(emailAlternative);
                if (existingUser is null) continue;

                var calcHours = user.TotalCompletedWork - 160;
                existingUser.Hour -= calcHours;
                WorkTimersBuilder.SetRamark(existingUser, calcHours, "Horas removida via sistema", existingUser.Name);

                await _userTimerRepository.UpsertAsync(new UserTimerInformation
                {
                    Id = existingUser.Id,
                    EmailAlternative = emailAlternative,
                    Email = existingUser.Email,
                    Hour = existingUser.Hour,
                    Name = name,
                    Remarks = existingUser.Remarks
                });
            }
            await _workTimerRepository.DeleteAllTaskByFileNameAsync(importedFile.FileName);

            await _workTimerImportedRepository.DeleteTaskByFileNameAsync(importedFile.FileName);

            return Unit.Value;
        }
        catch (FileNotFoundException ex)
        {
            throw new ApplicationException($"File not found: {ex.Message}", ex);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
namespace Domain.Commands.v1.WorkTimerImported.Upload;

public class UploadWorkTimerImportedCommandHandler(
    IWorkTimerImportedRepository _workTimerImportedRepository,
    IWorkTimerRepository _workTimerRepository,
    IUserTimerRepository _userTimerRepository
) : IRequestHandler<UploadWorkTimerImportedCommand, Unit>
{
    public async Task<Unit> Handle(UploadWorkTimerImportedCommand request, CancellationToken cancellationToken)
    {
        try
        {
            #region WorkTimerImported
            var workTimerImported = new WorkTimerImportedInformation
            {
                CreatedAt = DateTime.Now,
                FileName = request.FileName,
                Month = request.Month,
                Year = request.Year,
            };

            var inserted = await _workTimerImportedRepository.InsertIfNotExistsAsync(workTimerImported);

            if (!inserted)
                return Unit.Value;

            #endregion

            #region WorkTimer
            var fileName = Path.GetFileNameWithoutExtension(request.File.FileName).Trim();
            var records = await WorkTimersBuilder.FileImportAsync(request.File, cancellationToken);

            foreach (var record in records)
            {
                record.FileName = fileName;
                record.CreatedAt = DateTime.Now;

                var taskExists = await _workTimerRepository.ExistTaskAsync(record.ID);

                if (!taskExists)
                    continue;

                await _workTimerRepository.AddAsync(record);
            }
            #endregion

            #region UserTimer
            var users = records.GroupBy(x => x.AssignedTo).OrderBy(x => x.First().CreatedBy);

            foreach (var user in users)
            {
                var (name, email) = WorkTimersBuilder.ExtractNameAndEmail(user.First().AssignedTo);

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
                    continue;

                var valorASomar = WorkTimersBuilder.CalculateExtraHours(user);

                var userTimer = await _userTimerRepository.FindEmailAsync(email);

                if (userTimer is not null)
                {
                    userTimer.Hour += valorASomar;
                    await _userTimerRepository.UpsertUserTimerAsync(userTimer);
                }
                else
                {
                    await _userTimerRepository.InsertUserTimerAsync(new UserTimerInformation
                    {
                        Email = email,
                        Hour = valorASomar,
                        Name = name,
                        UpdateAt = DateTime.Now
                    });
                }
            }
            #endregion
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return Unit.Value;
    }
}
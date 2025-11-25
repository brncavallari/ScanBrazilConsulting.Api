namespace Domain.Interfaces.v1.Repositories.UserTimer;

public interface IUserTimerRepository
{
    Task InsertUserTimerAsync(UserTimerInformation workInformation);
    Task<UserTimerInformation> FindEmailAsync(string email);
    Task UpsertUserTimerAsync(UserTimerInformation workInformation);
    Task<IEnumerable<UserTimerInformation>> FindAllUserInformationAsync();
}

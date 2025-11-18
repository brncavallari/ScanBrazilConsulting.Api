namespace Domain.Interfaces.v1.UserTimer;

public interface IUserTimerRepository
{
    Task InsertUserTimerAsync(UserTimerInformation workInformation);
    Task<UserTimerInformation> FindEmailAsync(string email);
    Task UpsertUserTimerAsync(UserTimerInformation workInformation);
    Task<IReadOnlyList<UserTimerInformation>> GetAllUserInformation();
}

namespace Domain.Interfaces.v1.Repositories.UserTimer;

public interface IUserTimerRepository
{
    Task AddAsync(UserTimerInformation workInformation);
    Task<UserTimerInformation> FindByEmailAsync(string email);
    Task UpsertAsync(UserTimerInformation workInformation);
    Task<IEnumerable<UserTimerInformation>> FindAllAsync();
    Task UpdateAsync(UserTimerInformation userInformation);
}

using MongoDB.Bson;

namespace Domain.Interfaces.v1.Repositories.UserTimer;

public interface IUserTimerRepository
{
    Task AddAsync(UserTimerInformation workInformation);
    Task<UserTimerInformation> FindByEmailAlternativeAsync(string emailAlternative);
    Task UpsertAsync(UserTimerInformation workInformation);
    Task<IEnumerable<UserTimerInformation>> FindAllAsync();
    Task UpdateAsync(UserTimerInformation userInformation);
    Task<UserTimerInformation> FindByEmailAsync(string email);
    Task<UserTimerInformation> FindByIdAsync(ObjectId id);
    Task CreateOrUpdateAsync(UserTimerInformation id);
    Task DeleteByIdAsync(ObjectId id);
}

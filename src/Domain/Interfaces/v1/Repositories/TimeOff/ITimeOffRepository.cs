using Domain.Entities.MongoDb.v1.TimeOff;

namespace Domain.Interfaces.v1.Repositories.TimeOff;
public interface ITimeOffRepository
{
    Task AddAsync(TimeOffInformation timeOffInformation);
}

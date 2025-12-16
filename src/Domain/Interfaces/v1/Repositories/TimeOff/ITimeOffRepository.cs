using Domain.Entities.MongoDb.v1.TimeOff;

namespace Domain.Interfaces.v1.Repositories.TimeOff;
public interface ITimeOffRepository
{
    Task AddAsync(TimeOffInformation timeOffInformation);
    Task<IEnumerable<TimeOffInformation>> FindAllAsync();
    Task<IEnumerable<TimeOffInformation>> FindByEmailAsync(string email);
    Task<TimeOffInformation> FindByProtocolAsync(string protocol);
    Task RemoveByProtocol(string protocol);
    Task ApproveOrRejectAsync(string description, string approver, string protocol, bool isApprove);
}

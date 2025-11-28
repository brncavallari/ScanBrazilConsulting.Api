using Domain.Interfaces.v1.Repositories.UserTimer;
using MediatR;

namespace Infrastructure.Data.Query.Queries.v1.UserTimer.GetAllUserTimer;
public class GetAllUserTimerQueryHandler(
    IUserTimerRepository userTimerRepository) : IRequestHandler<GetAllUserTimerQuery, IEnumerable<GetAllUserTimerQueryResponse>>
{
    private readonly IUserTimerRepository _userTimerRepository = userTimerRepository;
    public async Task<IEnumerable<GetAllUserTimerQueryResponse>> Handle(GetAllUserTimerQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var usersInformation = await _userTimerRepository.FindAllAsync();

            if (usersInformation is null || !usersInformation.Any()) return [];

            return usersInformation.Select(user => (GetAllUserTimerQueryResponse)user);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
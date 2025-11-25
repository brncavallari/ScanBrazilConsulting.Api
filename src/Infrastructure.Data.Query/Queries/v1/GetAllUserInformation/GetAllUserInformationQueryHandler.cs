using Domain.Interfaces.v1.Repositories.UserTimer;
using MediatR;

namespace Infrastructure.Data.Query.Queries.v1.GetAllUserInformation;

public class GetAllUserInformationQueryHandler(IUserTimerRepository userTimerRepository) : IRequestHandler<GetAllUserInformationQuery, List<GetAllUserInformationQueryResponse>>
{
    private readonly IUserTimerRepository _userTimerRepository = userTimerRepository;

    public async Task<List<GetAllUserInformationQueryResponse>> Handle(GetAllUserInformationQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var usersInformation = await _userTimerRepository.GetAllUserInformation();

            if (usersInformation is null || usersInformation.Count == 0) return [];

            var responses = usersInformation.Select(user => (GetAllUserInformationQueryResponse)user).ToList();

            return responses;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
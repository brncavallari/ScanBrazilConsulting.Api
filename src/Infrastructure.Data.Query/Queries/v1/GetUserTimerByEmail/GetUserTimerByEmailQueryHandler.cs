using Domain.Interfaces.v1.Repositories.UserTimer;
using Infrastructure.Data.Service.Interfaces.v1.Microsoft;
using Infrastructure.Data.Service.Services.Microsoft;
using MediatR;

namespace Infrastructure.Data.Query.Queries.v1.GetUserTimerByEmail;
public sealed class GetUserTimerByEmailQueryHandler(
     IUserTimerRepository _userTimerRepository,
     IMicrosoftServiceClient _microsoftServiceClient) : IRequestHandler<GetUserTimerByEmailQuery, GetUserTimerByEmailQueryResponse>
{
    public async Task<GetUserTimerByEmailQueryResponse> Handle(GetUserTimerByEmailQuery getWorkTimerByEmailQuery, CancellationToken cancellationToken)
    {
        try
        {
            var userInfos = await _microsoftServiceClient.GetUserInformationAsync(
                new MicrosoftServiceRequest(getWorkTimerByEmailQuery.Token)
            );

            var userTimer = await _userTimerRepository.FindEmailAsync(userInfos.Email);

            GetUserTimerByEmailQueryResponse getUserTimerByEmailResponse = userTimer;

            return getUserTimerByEmailResponse;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

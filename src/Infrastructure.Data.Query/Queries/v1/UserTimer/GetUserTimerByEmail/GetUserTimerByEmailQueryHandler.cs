using Domain.Interfaces.v1.Context;
using Domain.Interfaces.v1.Repositories.UserTimer;
using MediatR;

namespace Infrastructure.Data.Query.Queries.v1.UserTimer.GetUserTimerByEmail;
public sealed class GetUserTimerByEmailQueryHandler(
     IUserTimerRepository _userTimerRepository,
     IUserContext _userContext) : IRequestHandler<GetUserTimerByEmailQuery, GetUserTimerByEmailQueryResponse>
{
    public async Task<GetUserTimerByEmailQueryResponse> Handle(GetUserTimerByEmailQuery getWorkTimerByEmailQuery, CancellationToken cancellationToken)
    {
        try
        {
            var userTimer = await _userTimerRepository.FindByEmailAlternativeAsync(
                _userContext.Email
            );

            GetUserTimerByEmailQueryResponse getUserTimerByEmailResponse = userTimer;

            return getUserTimerByEmailResponse;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

using Domain.Interfaces.v1.UserTimer;
using MediatR;

namespace Infrastructure.Data.Query.Queries.v1.GetUserTimerByEmail;
public sealed class GetUserTimerByEmailQueryHandler(
     IUserTimerRepository _userTimerRepository) : IRequestHandler<GetUserTimerByEmailQuery, GetUserTimerByEmailQueryResponse>
{
    public async Task<GetUserTimerByEmailQueryResponse> Handle(GetUserTimerByEmailQuery getWorkTimerByEmailQuery, CancellationToken cancellationToken)
    {
		try
		{
			var userTimer = await _userTimerRepository.FindEmailAsync(getWorkTimerByEmailQuery.Email);

            GetUserTimerByEmailQueryResponse getUserTimerByEmailResponse = userTimer;

            return getUserTimerByEmailResponse;
        }
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
    }
}

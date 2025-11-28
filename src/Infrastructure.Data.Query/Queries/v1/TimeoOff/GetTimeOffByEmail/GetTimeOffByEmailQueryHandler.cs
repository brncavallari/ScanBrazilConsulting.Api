using Domain.Interfaces.v1.Context;
using Domain.Interfaces.v1.Repositories.TimeOff;
using MediatR;

namespace Infrastructure.Data.Query.Queries.v1.TimeoOff.GetTimeOffByEmail;
public sealed class GetTimeOffByEmailQueryHandler(
	ITimeOffRepository _timeOffRepository,
	IUserContext _userContext) : IRequestHandler<GetTimeOffByEmailQuery, IEnumerable<GetTimeOffByEmailQueryResponse>>
{
    public async Task<IEnumerable<GetTimeOffByEmailQueryResponse>> Handle(GetTimeOffByEmailQuery getTimeOffByEmail, CancellationToken cancellationToken)
    {
		try
		{
			var timeOffs = await _timeOffRepository.FindByEmailAsync(_userContext.CompanyEmail);
            return timeOffs.Select(timeOff => (GetTimeOffByEmailQueryResponse)timeOff);
        }
		catch (Exception)
		{
			throw;
		}
    }
}

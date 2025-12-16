using Domain.Interfaces.v1.Repositories.TimeOff;
using MediatR;

namespace Infrastructure.Data.Query.Queries.v1.TimeoOff.GetAllTimeOff;
public sealed class GetAllTimeOffQueryHandler(
	ITimeOffRepository _timeOffRepository) : IRequestHandler<GetAllTimeOffQuery, IEnumerable<GetAllTimeOffQueryResponse>>
{
    public async Task<IEnumerable<GetAllTimeOffQueryResponse>> Handle(GetAllTimeOffQuery getAllTimeOffQuery, CancellationToken cancellationToken)
    {
		try
		{
			var timeOffsInformation = await _timeOffRepository.FindAllAsync();
            return timeOffsInformation.Select(timeOff => (GetAllTimeOffQueryResponse)timeOff);
        }
		catch (Exception)
		{
			throw;
		}
    }
}

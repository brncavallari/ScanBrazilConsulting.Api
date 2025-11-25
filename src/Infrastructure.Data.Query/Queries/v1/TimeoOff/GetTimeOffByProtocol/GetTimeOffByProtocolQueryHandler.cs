using Domain.Interfaces.v1.Repositories.TimeOff;
using MediatR;

namespace Infrastructure.Data.Query.Queries.v1.TimeoOff.GetTimeOffByProtocol;
public sealed class GetTimeOffByProtocolQueryHandler(
	ITimeOffRepository _timeOffRepository) : IRequestHandler<GetTimeOffByProtocolQuery, GetTimeOffByProtocolQueryResponse>
{
    public async Task<GetTimeOffByProtocolQueryResponse> Handle(GetTimeOffByProtocolQuery getTimeOffByProtocol, CancellationToken cancellationToken)
    {
		try
		{
			var timeOffInformation = await _timeOffRepository.FindByProtocolAsync(
				getTimeOffByProtocol.Protocol
			);

			return (GetTimeOffByProtocolQueryResponse)timeOffInformation;
        }
		catch (Exception)
		{

			throw;
		}
    }
}

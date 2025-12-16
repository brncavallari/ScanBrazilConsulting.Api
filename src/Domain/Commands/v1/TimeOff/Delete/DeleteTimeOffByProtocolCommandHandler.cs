using Domain.Interfaces.v1.Repositories.TimeOff;

namespace Domain.Commands.v1.TimeOff.Delete;
public sealed class DeleteTimeOffByProtocolCommandHandler(
	ITimeOffRepository _timeOffRepository) : IRequestHandler<DeleteTimeOffByProtocolCommand>
{
    public async Task Handle(DeleteTimeOffByProtocolCommand deleteTimeOffByProtocol, CancellationToken cancellationToken)
    {
		try
		{
			var timeOff = await _timeOffRepository.FindByProtocolAsync(deleteTimeOffByProtocol.Protocol) ?? 
				throw new Exception();

            await _timeOffRepository.RemoveByProtocol(
				deleteTimeOffByProtocol.Protocol
			);
        }
		catch (Exception)
		{
			throw;
		}
    }
}

using Domain.Entities.MongoDb.v1.TimeOff;
using Domain.Interfaces.v1.Repositories.TimeOff;

namespace Domain.Commands.v1.TimeOff.Create;
public sealed class CreateTimeOffCommandHandler(
	ITimeOffRepository _timeOffRepository) : IRequestHandler<CreateTimeOffCommand>
{
    public async Task Handle(CreateTimeOffCommand createTimeOffCommand, CancellationToken cancellationToken)
    {
		try
		{
            TimeOffInformation timeOffInformation = createTimeOffCommand;

			await _timeOffRepository.AddAsync(
				timeOffInformation
			);
        }
		catch (Exception)
		{
			throw;
		}
    }
}

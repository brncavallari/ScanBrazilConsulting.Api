using CrossCutting.Configuration;
using CrossCutting.Configuration.AppModels;
using Domain.Entities.MongoDb.v1.TimeOff;
using Domain.Interfaces.v1.Repositories.TimeOff;
using Domain.Templates.v1;
using Infrastructure.Service.Interfaces.v1.Smtp;

namespace Domain.Commands.v1.TimeOff.Create;
public sealed class CreateTimeOffCommandHandler(
    ITimeOffRepository _timeOffRepository,
    ISmtpServiceClient _smtpServiceClient) : IRequestHandler<CreateTimeOffCommand>
{
    private readonly EmailSettings _emailSettings = AppSettings.Settings.EmailSettings;
    public async Task Handle(CreateTimeOffCommand createTimeOffCommand, CancellationToken cancellationToken)
    {
        try
        {
            TimeOffInformation timeOffInformation = createTimeOffCommand;

            await _timeOffRepository.AddAsync(
                timeOffInformation
            );

            var body = EmailTemplate.TimeOff(
                createTimeOffCommand,
                timeOffInformation.Protocol
            );

            await _smtpServiceClient.SendEmailAsync(
                _emailSettings.Approver,
                EmailTemplate.Subject,
                body
            );
        }
        catch (Exception)
        {
            throw;
        }
    }
}

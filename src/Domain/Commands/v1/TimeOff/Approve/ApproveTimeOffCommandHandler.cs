using Domain.Constants.v1;
using Domain.Interfaces.v1.Context;
using Domain.Interfaces.v1.Repositories.TimeOff;
using Domain.Interfaces.v1.Repositories.UserTimer;
using Infrastructure.Service.Interfaces.v1.Smtp;

namespace Domain.Commands.v1.TimeOff.Approve;
public sealed class ApproveTimeOffCommandHandler(
    ITimeOffRepository _timeOffRepository,
    IUserContext _userContext,
    ISmtpServiceClient _smtpServiceClient,
    IUserTimerRepository _userTimerRepository) : IRequestHandler<ApproveTimeOffCommand>
{
    public async Task Handle(ApproveTimeOffCommand approveTimeOffCommand, CancellationToken cancellationToken)
    {
		try
		{
            var timeOff = await _timeOffRepository.FindByProtocolAsync(approveTimeOffCommand.Protocol) ??
                throw new Exception();

            var userTimer = await _userTimerRepository.FindEmailAsync(_userContext.UserEmail);

            userTimer.Subtract(
                timeOff.Hour);

            await _userTimerRepository.UpdateUserTimerAsync(
                userTimer);

            await _timeOffRepository.ApproveOrRejectTimeOffAsync(
                description: approveTimeOffCommand.Description,
                protocol: approveTimeOffCommand.Protocol,
                approver: _userContext.UserName,
                isApprove: true
            );

            await SendEmailAsync(
                approveTimeOffCommand.Protocol,
                timeOff.UserEmail
            );
        }
		catch (Exception)
		{
			throw;
		}
    }

    private async Task SendEmailAsync(
        string protocol,
        string email)
    {
        var body = EmailConstant.TimeOffTemplateApproved(
            protocol,
            _userContext.UserName
        );

        await _smtpServiceClient.SendEmailAsync(
            email,
            EmailConstant.SubjectApproved,
            body
        );
    }
}

using CrossCutting.Configuration;
using CrossCutting.Configuration.AppModels;
using Domain.Constants.v1;
using Domain.Interfaces.v1.Context;
using Domain.Interfaces.v1.Repositories.TimeOff;
using Infrastructure.Service.Interfaces.v1.Smtp;

namespace Domain.Commands.v1.TimeOff.Reject;
public sealed class RejectTimeOffCommandHandler(
    ITimeOffRepository _timeOffRepository,
    IUserContext _userContext,
    ISmtpServiceClient _smtpServiceClient) : IRequestHandler<RejectTimeOffCommand>
{
    public async Task Handle(RejectTimeOffCommand rejectTimeOffCommand, CancellationToken cancellationToken)
    {
        try
        {
            var timeOff = await _timeOffRepository.FindByProtocolAsync(rejectTimeOffCommand.Protocol) ??
                throw new Exception();

            await _timeOffRepository.ApproveOrRejectTimeOffAsync(
                description: rejectTimeOffCommand.Description,
                protocol: rejectTimeOffCommand.Protocol,
                approver: _userContext.UserName,
                isApprove: false
            );

            await SendEmailAsync(
                rejectTimeOffCommand,
                timeOff.UserEmail
            );
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task SendEmailAsync(
        RejectTimeOffCommand rejectTimeOffCommand,
        string email)
    {
        var body = EmailConstant.TimeOffTemplateRejected(
            rejectTimeOffCommand,
            _userContext.UserName
        );

        await _smtpServiceClient.SendEmailAsync(
            email,
            EmailConstant.SubjectRejected,
            body
        );
    }
}

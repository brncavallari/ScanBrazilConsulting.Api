using Domain.Interfaces.v1.Context;
using Domain.Interfaces.v1.Repositories.TimeOff;
using Domain.Templates.v1;
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

            await _timeOffRepository.ApproveOrRejectAsync(
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
        var body = EmailTemplate.Rejected(
            rejectTimeOffCommand,
            _userContext.UserName
        );

        await _smtpServiceClient.SendEmailAsync(
            email,
            EmailTemplate.SubjectRejected,
            body
        );
    }
}

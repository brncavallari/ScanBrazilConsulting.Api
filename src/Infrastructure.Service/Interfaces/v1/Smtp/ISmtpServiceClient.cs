using Infrastructure.Service.Services.Smtp;

namespace Infrastructure.Service.Interfaces.v1.Smtp;
public interface ISmtpServiceClient
{
    Task SendEmailAsync(EmailRequest emailRequest);
    Task SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true);
}

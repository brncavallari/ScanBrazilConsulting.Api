using CrossCutting.Configuration;
using CrossCutting.Configuration.AppModels;
using Infrastructure.Service.Interfaces.v1.Smtp;
using Infrastructure.Service.Services.Smtp;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Service.ServiceHandlers.v1.Smtp;
public sealed class SmtpServiceClient : ISmtpServiceClient
{
    private readonly EmailSettings _emailSettings = AppSettings.Settings.EmailSettings;
    public async Task SendEmailAsync(EmailRequest emailRequest)
    {
        try
        {
            using var mailMessage = new MailMessage();
            var smtpClient = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port)
            {
                Credentials = new NetworkCredential(_emailSettings.UserName, _emailSettings.Password),
                EnableSsl = _emailSettings.EnableSsl,
                UseDefaultCredentials = _emailSettings.UseDefaultCredentials
            };

            mailMessage.From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName);

            mailMessage.To.Add(emailRequest.ToEmail);

            if (emailRequest.Cc != null && emailRequest.Cc.Any())
            {
                foreach (var ccEmail in emailRequest.Cc)
                {
                    mailMessage.CC.Add(ccEmail);
                }
            }

            if (emailRequest.Bcc != null && emailRequest.Bcc.Any())
            {
                foreach (var bccEmail in emailRequest.Bcc)
                {
                    mailMessage.Bcc.Add(bccEmail);
                }
            }

            mailMessage.Subject = emailRequest.Subject;
            mailMessage.Body = emailRequest.Body;
            mailMessage.IsBodyHtml = emailRequest.IsHtml;
            mailMessage.Priority = MailPriority.Normal;

            if (emailRequest.Attachments != null && emailRequest.Attachments.Any())
            {
                foreach (var attachment in emailRequest.Attachments)
                {
                    var stream = new MemoryStream(attachment.Content);
                    var mailAttachment = new System.Net.Mail.Attachment(stream, attachment.FileName, attachment.ContentType);
                    mailMessage.Attachments.Add(mailAttachment);
                }
            }

            smtpClient.Credentials = new NetworkCredential(_emailSettings.UserName, _emailSettings.Password);
            smtpClient.EnableSsl = _emailSettings.EnableSsl;
            smtpClient.UseDefaultCredentials = _emailSettings.UseDefaultCredentials;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Timeout = 30000;

            await smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            throw new Exception($"Falha no envio do email: {ex.Message}", ex);
        }
    }
    public async Task SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true)
    {
        var emailRequest = new EmailRequest
        {
            ToEmail = toEmail,
            Subject = subject,
            Body = body,
            IsHtml = isHtml,
            Cc = _emailSettings.Cc
        };

        await SendEmailAsync(emailRequest);
    }
}

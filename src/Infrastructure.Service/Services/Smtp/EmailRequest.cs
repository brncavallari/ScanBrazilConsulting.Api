namespace Infrastructure.Service.Services.Smtp;
public sealed class EmailRequest
{
    public string ToEmail { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool IsHtml { get; set; } = true;
    public IEnumerable<string> Cc { get; set; }
    public IEnumerable<string> Bcc { get; set; }
    public IEnumerable<Attachment> Attachments { get; set; }

    public class Attachment
    {
        public byte[] Content { get; set; } = [];
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
    }
}

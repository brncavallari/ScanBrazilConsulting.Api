using System.Text.Json.Serialization;

namespace CrossCutting.Configuration.AppModels;
public sealed class EmailSettings
{
    [JsonPropertyName("smtpServer")]
    public string SmtpServer { get; set; } = string.Empty;
    [JsonPropertyName("port")]
    public int Port { get; set; }
    [JsonPropertyName("senderName")]
    public string SenderName { get; set; } = string.Empty;
    [JsonPropertyName("senderEmail")]
    public string SenderEmail { get; set; } = string.Empty;
    [JsonPropertyName("userName")]
    public string UserName { get; set; } = string.Empty;
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
    [JsonPropertyName("enableSsl")]
    public bool EnableSsl { get; set; }
    [JsonPropertyName("useDefaultCredentials")]
    public bool UseDefaultCredentials { get; set; }
    [JsonPropertyName("approver")]
    public string Approver { get; set; }
    [JsonPropertyName("cc")]
    public IEnumerable<string> Cc { get; set; }
}

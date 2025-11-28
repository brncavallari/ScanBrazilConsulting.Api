using System.Text.Json.Serialization;

namespace Infrastructure.Service.Services.Microsoft;
public sealed class MicrosoftServiceResponse
{
    [JsonPropertyName("mail")]
    public string Email { get; set; }

    [JsonPropertyName("userPrincipalName")]
    public string CompanyEmail { get; set; }

    [JsonPropertyName("employeeId")]
    public string Employee { get; set; }

    [JsonPropertyName("department")]
    public string Department { get; set; }

    [JsonPropertyName("mailNickname")]
    public string UserName { get; set; }

    [JsonPropertyName("displayName")]
    public string Name { get; set; }

    [JsonPropertyName("jobTitle")]
    public string Job { get; set; }
}

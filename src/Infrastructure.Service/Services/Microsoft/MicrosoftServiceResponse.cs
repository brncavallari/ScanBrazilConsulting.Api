using System.Text.Json.Serialization;

namespace Infrastructure.Data.Service.Services.Microsoft;

public class MicrosoftServiceResponse
{
    [JsonPropertyName("mail")]
    public string Email { get; set; }

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

using System.Net;
using System.Text.Json.Serialization;

namespace Infrastructure.Data.Service.Services.Base;

public class ResponseBase
{
    [JsonIgnore]
    public HttpStatusCode HttpStatusCode { get; set; }

    [JsonPropertyName("notifications")]
    public object Notifications { get; set; }

    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    public string GetNotificationMessage()
    {
        try
        {
            var response = Notifications != null ? Notifications.ToString() : "";
            return response;
        }
        catch
        {
            return "Erro ao processar a solicitação!";
        }
    }
}
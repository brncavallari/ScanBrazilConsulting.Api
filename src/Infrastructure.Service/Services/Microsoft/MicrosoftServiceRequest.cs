namespace Infrastructure.Data.Service.Services.Microsoft;

public class MicrosoftServiceRequest(
    string token)
{
    public string Token { get; set; } = token;
}
using Infrastructure.Data.Service.Interfaces.v1.Microsoft;
using Infrastructure.Data.Service.Services.Microsoft;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Infrastructure.Data.Service.ServiceHandlers.v1.Microsoft;

public class MicrosoftServiceClient : IMicrosoftServiceClient
{
    private readonly HttpClient _httpClient;

    private const string ApiBaseUrl = "https://graph.microsoft.com/";

    public MicrosoftServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        if (_httpClient.BaseAddress == null)
            _httpClient.BaseAddress = new Uri(ApiBaseUrl);
    }

    public async Task<MicrosoftServiceResponse> GetUserInformationAsync(MicrosoftServiceRequest request)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", request.Token);

        HttpResponseMessage response = await _httpClient.GetAsync("beta/me");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<MicrosoftServiceResponse>();

            return content ?? throw new Exception("Resposta do serviço Microsoft inválida ou vazia.");
        }

        if (response.StatusCode == HttpStatusCode.Unauthorized)
            throw new Exception("Token de acesso inválido ou expirado.");
        throw new Exception($"Erro ao acessar Microsoft API. Status: {response.StatusCode}.");
    }
}
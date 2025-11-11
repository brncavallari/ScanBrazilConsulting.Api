using Infrastructure.Data.Service.Services.Microsoft;

namespace Infrastructure.Data.Service.Interfaces.v1.Microsoft;

public interface IMicrosoftServiceClient
{
    Task<MicrosoftServiceResponse> GetUserInformationAsync(MicrosoftServiceRequest request);
}

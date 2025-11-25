using Domain.Interfaces.v1.Context;
namespace API.Context.v1;
public sealed class UserContext(
    IHttpContextAccessor _httpContextAccessor) : IUserContext
{
    public string UserName => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
    public string UserEmail => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
    public string UserRole => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
}

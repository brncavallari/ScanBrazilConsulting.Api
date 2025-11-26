using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace API.Filter.v1;
public class TokenValidationMiddleware(
    RequestDelegate next,
    IMicrosoftServiceClient microsoftServiceClient,
    IMemoryCache _cache)
{
    private readonly RequestDelegate _next = next;
    private readonly IMicrosoftServiceClient _microsoftServiceClient = microsoftServiceClient;

    public async Task InvokeAsync(HttpContext context)
    {
        var authorizationHeader = context.Request.Headers.Authorization.ToString();

        if (!string.IsNullOrWhiteSpace(authorizationHeader) &&
            authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            var token = authorizationHeader["Bearer ".Length..].Trim();

            try
            {
                var handler = new JwtSecurityTokenHandler();
                if (handler.CanReadToken(token))
                {
                    var cacheKey = $"userinfo_{Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(token))}";

                    if (!_cache.TryGetValue(cacheKey, out MicrosoftServiceResponse userInfo))
                    {
                        
                        userInfo = await _microsoftServiceClient.GetUserInformationAsync(
                            new MicrosoftServiceRequest(token));

                        var cacheOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromMinutes(15))
                            .SetAbsoluteExpiration(TimeSpan.FromHours(2));

                        _cache.Set(cacheKey, userInfo, cacheOptions);
                    }

                    var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);

                    if (!string.IsNullOrEmpty(userInfo.Email))
                        identity.AddClaim(new Claim(ClaimTypes.Email, userInfo.Email));
                    if (!string.IsNullOrEmpty(userInfo.Name))
                        identity.AddClaim(new Claim(ClaimTypes.Name, userInfo.Name));
                    if (!string.IsNullOrEmpty(userInfo.Job))
                        identity.AddClaim(new Claim(ClaimTypes.Role, userInfo.Job));

                    if (identity.Claims.Any())
                        context.User.AddIdentity(identity);
                }
                else
                {
                    await WriteErrorResponse(context, 401, "Token malformado ou inválido");
                    return;
                }
            }
            catch (UnauthorizedAccessException)
            {
                await WriteErrorResponse(context, 401, "Token inválido ou expirado");
                return;
            }
            catch (HttpRequestException)
            {
                await WriteErrorResponse(context, 503, "Serviço de autenticação indisponível");
                return;
            }
            catch (Exception)
            {
                await WriteErrorResponse(context, 500, "Erro interno na validação do token");
                return;
            }
        }
        else
        {
            await WriteErrorResponse(context, 401, "Token de acesso é obrigatório");
            return;
        }

        await _next(context);
    }
    private static async Task WriteErrorResponse(HttpContext context, int statusCode, string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var errorResponse = new
        {
            statusCode,
            message,
            timestamp = DateTime.UtcNow
        };

        var jsonResponse = JsonSerializer.Serialize(errorResponse);
        await context.Response.WriteAsync(jsonResponse);
    }
}
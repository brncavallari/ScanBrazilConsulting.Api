namespace API.Filter.v1
{
    public class TokenValidationMiddleware(
        RequestDelegate next,
        IMicrosoftServiceClient microsoftServiceClient)
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
                        var userInfo = await _microsoftServiceClient.GetUserInformationAsync(
                            new MicrosoftServiceRequest(token));

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
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            await _next(context);
        }
    }
}
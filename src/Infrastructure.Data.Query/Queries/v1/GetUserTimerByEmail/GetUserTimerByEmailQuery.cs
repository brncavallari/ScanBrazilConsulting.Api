using MediatR;

namespace Infrastructure.Data.Query.Queries.v1.GetUserTimerByEmail;
public sealed class GetUserTimerByEmailQuery(
    string token) : IRequest<GetUserTimerByEmailQueryResponse>
{
    public string Token { get; set; } = token["Bearer ".Length..].Trim();
}

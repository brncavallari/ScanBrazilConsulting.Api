using MediatR;

namespace Infrastructure.Data.Query.Queries.v1.GetUserTimerByEmail;
public sealed class GetUserTimerByEmailQuery : IRequest<GetUserTimerByEmailQueryResponse>
{
    public string Email { get; set; }
}

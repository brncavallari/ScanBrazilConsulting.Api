using MediatR;

namespace Infrastructure.Data.Query.Queries.v1.TimeoOff.GetTimeOffByEmail;
public sealed class GetTimeOffByEmailQuery : IRequest<IEnumerable<GetTimeOffByEmailQueryResponse>>
{ }

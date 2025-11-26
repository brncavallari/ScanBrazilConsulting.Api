using MediatR;

namespace Infrastructure.Data.Query.Queries.v1.TimeoOff.GetAllTimeOff;
public sealed class GetAllTimeOffQuery : IRequest<IEnumerable<GetAllTimeOffQueryResponse>>
{ }

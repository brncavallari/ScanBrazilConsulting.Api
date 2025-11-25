using MediatR;

namespace Infrastructure.Data.Query.Queries.v1.TimeoOff.GetTimeOffByProtocol;
public sealed class GetTimeOffByProtocolQuery(
    string protocol) : IRequest<GetTimeOffByProtocolQueryResponse>
{
    public string Protocol { get; set; } = protocol;
}

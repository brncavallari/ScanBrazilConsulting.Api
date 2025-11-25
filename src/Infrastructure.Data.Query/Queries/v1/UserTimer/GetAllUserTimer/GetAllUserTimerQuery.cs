using MediatR;

namespace Infrastructure.Data.Query.Queries.v1.UserTimer.GetAllUserTimer;

public class GetAllUserTimerQuery : IRequest<IEnumerable<GetAllUserTimerQueryResponse>> { }
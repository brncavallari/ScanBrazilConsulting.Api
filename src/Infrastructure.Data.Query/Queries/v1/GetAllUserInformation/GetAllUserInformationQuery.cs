using MediatR;

namespace Infrastructure.Data.Query.Queries.v1.GetAllUserInformation;

public class GetAllUserInformationQuery : IRequest<List<GetAllUserInformationQueryResponse>> { }
using Domain.Interfaces.v1.Repositories.WorkTimerImported;
using MediatR;

namespace Infrastructure.Data.Query.Queries.v1.GetAllWorkTimersImported;

public class GetAllWorkTimersImportedQueryHandler(
    IWorkTimerImportedRepository _workTimerImportedRepository
    ) : IRequestHandler<GetAllWorkTimersImportedQuery, List<GetAllWorkTimersImportedQueryResponse>>
{
    public async Task<List<GetAllWorkTimersImportedQueryResponse>> Handle(GetAllWorkTimersImportedQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var workTimersImported = await _workTimerImportedRepository.GetAllWorkTimersImported();

            if (workTimersImported is null || workTimersImported.Count == 0)  return [];

            var responses = GetAllWorkTimersImportedQueryResponse.FromEntityList(workTimersImported);

            return responses;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

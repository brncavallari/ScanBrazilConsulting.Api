using Domain.Interfaces.v1.Repositories.WorkTimerImported;
using MediatR;

namespace Infrastructure.Data.Query.Queries.v1.WorkTimer.GetAllWorkTimersImported;
public class GetAllWorkTimersImportedQueryHandler(
    IWorkTimerImportedRepository _workTimerImportedRepository
    ) : IRequestHandler<GetAllWorkTimersImportedQuery, List<GetAllWorkTimersImportedQueryResponse>>
{
    public async Task<List<GetAllWorkTimersImportedQueryResponse>> Handle(GetAllWorkTimersImportedQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var workTimersInformation = await _workTimerImportedRepository.FindAllAsync();

            if (workTimersInformation is null || workTimersInformation.Count == 0)  return [];

            var workTimersImportedResponse = GetAllWorkTimersImportedQueryResponse.Map(
                workTimersInformation);

            return workTimersImportedResponse;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

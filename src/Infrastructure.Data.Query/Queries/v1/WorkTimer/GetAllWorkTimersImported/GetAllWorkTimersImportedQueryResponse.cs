using Domain.Entities.MongoDb.v1.WorkTimerImported;

namespace Infrastructure.Data.Query.Queries.v1.WorkTimer.GetAllWorkTimersImported;

public class GetAllWorkTimersImportedQueryResponse
{
    public string Id { get; set; }
    public string FileName { get; set; }
    public string Year { get; set; }
    public string Month { get; set; }
    public DateTime CreatedAt { get; set; }

    public static implicit operator GetAllWorkTimersImportedQueryResponse(WorkTimerImportedInformation information)
    {
        if (information is null) return new GetAllWorkTimersImportedQueryResponse();

        return new()
        {
            Id = information.Id,
            FileName = information.FileName,
            Year = information.Year,
            Month = information.Month,
            CreatedAt = information.CreatedAt,
        };
    }

    public static List<GetAllWorkTimersImportedQueryResponse> Map(IEnumerable<WorkTimerImportedInformation> entities)
    {
        if (entities is null || !entities.Any()) return [];

        return [.. entities.Select(info => (GetAllWorkTimersImportedQueryResponse)info)];
    }
}
using Domain.Entities.MongoDb.v1.TimeOff;
using Domain.Enum.v1;

namespace Infrastructure.Data.Query.Queries.v1.TimeoOff.GetTimeOffByProtocol;
public sealed class GetTimeOffByProtocolQueryResponse
{
    public string Protocol { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string UserEmail { get; set; }
    public double Hour { get; set; }
    public TimeOffEnum Status { get; set; }
    public string Approver { get; set; }
    public string Remark { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }

    public static implicit operator GetTimeOffByProtocolQueryResponse(TimeOffInformation timeOffInformation)
    {
        if (timeOffInformation is null) return new GetTimeOffByProtocolQueryResponse();

        return new()
        {
            Protocol = timeOffInformation.Protocol,
            Approver = timeOffInformation.Approver,
            CreatedAt = timeOffInformation.CreatedAt,
            Status = timeOffInformation.Status,
            Description = timeOffInformation.Description,
            EndDate = timeOffInformation.EndDate,
            UserEmail = timeOffInformation.UserEmail,
            Hour = timeOffInformation.Hour,
            Remark = timeOffInformation.Remark,
            StartDate = timeOffInformation.StartDate
        };
    }
}
namespace Domain.Entities.MongoDb.v1.WorkTimer;
public sealed class WorkTimerInformation
{
    [BsonElement("id")]
    public string ID { get; set; }
    [BsonElement("fileName")]
    public string FileName { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("workItemType")]
    public string WorkItemType { get; set; }

    [BsonElement("state")]
    public string State { get; set; }

    [BsonElement("fabrica")]
    public string Fabrica { get; set; }
    [BsonElement("title")]
    public string Title { get; set; }

    [BsonElement("assignedTo")]
    public string AssignedTo { get; set; }

    [BsonElement("completedWork")]
    public string CompletedWork { get; set; }

    [BsonElement("originalEstimate")]
    public string OriginalEstimate { get; set; }

    [BsonElement("numeroDoEpico")]
    public string NumeroDoEpico { get; set; }

    [BsonElement("iterationPath")]
    public string IterationPath { get; set; }

    [BsonElement("createdDate")]
    public string CreatedDate { get; set; }

    [BsonElement("closedDate")]
    public string ClosedDate { get; set; }

    [BsonElement("changedDate")]
    public string ChangedDate { get; set; }

    [BsonElement("createdBy")]
    public string CreatedBy { get; set; }

    [BsonElement("closedBy")]
    public string ClosedBy { get; set; }

    [BsonElement("changedBy")]
    public string ChangedBy { get; set; }

    [BsonElement("rateCard")]
    public string RateCard { get; set; }

    [BsonElement("valorHora")]
    public string ValorHora { get; set; }

    [BsonElement("valorTotal")]
    public string ValorTotal { get; set; }
}

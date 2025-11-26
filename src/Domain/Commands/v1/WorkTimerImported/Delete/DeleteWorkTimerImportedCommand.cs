namespace Domain.Commands.v1.WorkTimerImported.Delete;

public sealed class DeleteWorkTimerImportedCommand : IRequest<Unit>
{
    public string Id { get; set; }
}

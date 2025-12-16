namespace Domain.Commands.v1.WorkTimerImported.Delete;

public sealed class DeleteWorkTimerImportedCommand(
    string id) : IRequest<Unit>
{
    public string Id { get; set; } = id;
}

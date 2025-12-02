namespace Domain.Commands.v1.UserTimer.Delete;
public sealed class DeleteCommand : IRequest
{
    public string Id { get; set; }
}

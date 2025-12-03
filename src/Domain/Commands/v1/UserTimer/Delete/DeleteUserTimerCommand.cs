using MongoDB.Bson;

namespace Domain.Commands.v1.UserTimer.Delete;
public sealed class DeleteUserTimerCommand(string id) : IRequest
{
    public ObjectId Id { get; set; } = ObjectId.Parse(id);
}

using MongoDB.Bson;

namespace Domain.Commands.v1.UserTimer.Delete;
public sealed class DeleteCommand(string id) : IRequest
{
    public ObjectId Id { get; set; } = ObjectId.Parse(id);
}

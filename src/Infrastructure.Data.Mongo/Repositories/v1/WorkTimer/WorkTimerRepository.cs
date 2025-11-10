using Domain.Entities.MongoDb.v1.WorkTimer;
using Domain.Interfaces.v1.WorkTimer;
using Infrastructure.Data.Mongo.Repositories.v1.Base;

namespace Infrastructure.Data.Mongo.Repositories.v1.WorkTimer;
public sealed class WorkTimerRepository(
    string collectionName) : MongoDbBaseRepository<WorkTimerInformation>(collectionName), IWorkTimerRepository
{
    private readonly string _collection = collectionName;

    public async Task CreateTesteAsync(
        WorkTimerInformation workInformation)
    {
        var collection = Database.GetCollection<WorkTimerInformation>(_collection);
        await collection.InsertOneAsync(new WorkTimerInformation() { Name = workInformation.Name });
    }
}
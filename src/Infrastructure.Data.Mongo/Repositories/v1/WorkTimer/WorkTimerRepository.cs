using Domain.Entities.MongoDb.v1.WorkTimer;
using Domain.Interfaces.v1.WorkTimer;
using Infrastructure.Data.Mongo.Repositories.v1.Base;
using MongoDB.Driver;

namespace Infrastructure.Data.Mongo.Repositories.v1.WorkTimer;
public sealed class WorkTimerRepository(
    string collectionName) : MongoDbBaseRepository<WorkTimerInformation>(collectionName), IWorkTimerRepository
{
    private readonly string _collection = collectionName;

    public async Task AddAsync(
        WorkTimerInformation workInformation)
    {
        var collection = Database.GetCollection<WorkTimerInformation>(_collection);
        await collection.InsertOneAsync(workInformation);
    }

    public async Task<List<WorkTimerInformation>> GetByFileNameAsync(string fileName)
    {
        var collection = Database.GetCollection<WorkTimerInformation>(_collection);

        var filter = Builders<WorkTimerInformation>.Filter.Eq(x => x.FileName, fileName);

        var result = await collection.Find(filter).ToListAsync();

        return result;
    }

    public async Task<bool> ExistTaskAsync(string id)
    {
        var collection = Database.GetCollection<WorkTimerInformation>(_collection);

        var filter = Builders<WorkTimerInformation>.Filter.Eq(x => x.ID, id);

        var result = await collection.Find(filter).AnyAsync();

        if (result)
            return false;

        return true;
    }

    public async Task<bool> DeleteAllTaskByFileNameAsync(string fileName)
    {
        var collection = Database.GetCollection<WorkTimerInformation>(_collection);

        var filter = Builders<WorkTimerInformation>.Filter.Eq(x => x.FileName, fileName);

        var result = await collection.DeleteManyAsync(filter);

        return result.DeletedCount > 0;
    }
}
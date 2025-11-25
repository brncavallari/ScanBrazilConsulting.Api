using Domain.Entities.MongoDb.v1.WorkTimer;
using Domain.Entities.MongoDb.v1.WorkTimerImported;
using Domain.Interfaces.v1.Repositories.WorkTimerImported;
using Infrastructure.Data.Mongo.Repositories.v1.Base;
using MongoDB.Driver;

namespace Infrastructure.Data.Mongo.Repositories.v1.WorkTimerImported;

public sealed class WorkTimerImportedRepository(string collectionName)
        : MongoDbBaseRepository<WorkTimerImportedInformation>(collectionName), IWorkTimerImportedRepository
{
    private readonly string _collection = collectionName;

    public async Task<bool> InsertIfNotExistsAsync(WorkTimerImportedInformation information)
    {
        var collection = Database.GetCollection<WorkTimerImportedInformation>(_collection);

        var filter = Builders<WorkTimerImportedInformation>.Filter.And(
            Builders<WorkTimerImportedInformation>.Filter.Eq(x => x.FileName, information.FileName),
            Builders<WorkTimerImportedInformation>.Filter.Eq(x => x.Year, information.Year),
            Builders<WorkTimerImportedInformation>.Filter.Eq(x => x.Month, information.Month)
        );

        var workImportedInformation = await collection.Find(filter).AnyAsync();

        if (workImportedInformation)
            return false;

        await collection.InsertOneAsync(information);
        return true;
    }

    public async Task<IReadOnlyList<WorkTimerImportedInformation>> GetAllWorkTimersImported()
    {
        var workTimerCollection = Database.GetCollection<WorkTimerImportedInformation>(_collection);

        var workTimers = await workTimerCollection
            .Find(Builders<WorkTimerImportedInformation>.Filter.Empty)
            .ToListAsync();

        return workTimers;
    }

    public async Task<WorkTimerImportedInformation> FindByIdAsync(string id)
    {
        var workTimerCollection = Database.GetCollection<WorkTimerImportedInformation>(_collection);

        var filter = Builders<WorkTimerImportedInformation>.Filter.Eq(x => x.Id, id);

        var workTimer = await workTimerCollection
            .Find(filter)
            .FirstOrDefaultAsync();

        return workTimer;
    }

    public async Task<bool> DeleteTaskByFileNameAsync(string fileName)
    {
        var collection = Database.GetCollection<WorkTimerImportedInformation>(_collection);

        var filter = Builders<WorkTimerImportedInformation>.Filter.Eq(x => x.FileName, fileName);

        var result = await collection.DeleteOneAsync(filter);

        return result.DeletedCount > 0;
    }
}
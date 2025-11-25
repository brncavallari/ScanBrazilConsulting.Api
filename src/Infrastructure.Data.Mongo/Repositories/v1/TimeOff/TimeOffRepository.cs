using Domain.Entities.MongoDb.v1.TimeOff;
using Domain.Entities.MongoDb.v1.WorkTimer;
using Domain.Interfaces.v1.Repositories.TimeOff;
using Infrastructure.Data.Mongo.Repositories.v1.Base;

namespace Infrastructure.Data.Mongo.Repositories.v1.TimeOff;
public sealed class TimeOffRepository(
    string collectionName) : MongoDbBaseRepository<TimeOffInformation>(collectionName), ITimeOffRepository
{
    private readonly string _collection = collectionName;

    public async Task AddAsync(
        TimeOffInformation timeOffInformation)
    {
        var collection = Database.GetCollection<TimeOffInformation>(_collection);
        await collection.InsertOneAsync(timeOffInformation);
    }
}

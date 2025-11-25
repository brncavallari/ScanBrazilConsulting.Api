using Domain.Entities.MongoDb.v1.TimeOff;
using Domain.Interfaces.v1.Repositories.TimeOff;
using Infrastructure.Data.Mongo.Repositories.v1.Base;
using MongoDB.Driver;

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

    public async Task<TimeOffInformation> FindByProtocolAsync(
        string protocol)
    {
        var collection = Database.GetCollection<TimeOffInformation>(_collection);

        var options = new FindOptions<TimeOffInformation>
        {
            Limit = 1
        };

        var filter = Builders<TimeOffInformation>.Filter.Eq(x => x.Protocol, protocol);

        var timeOff = (await collection.FindAsync(filter, options: options)).FirstOrDefaultAsync();

        return await timeOff;
    }

    public async Task<IEnumerable<TimeOffInformation>> FindAllTimeOffAsync()
    {
        var collection = Database.GetCollection<TimeOffInformation>(_collection);

        var options = new FindOptions<TimeOffInformation>
        {
            Sort = Builders<TimeOffInformation>.Sort.Ascending(x => x.Status)
        };

        var timeOffs = (await collection.FindAsync(Builders<TimeOffInformation>.Filter.Empty, options: options)).ToListAsync();

        return await timeOffs;
    }
}

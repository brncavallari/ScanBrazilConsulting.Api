using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Domain.Entities.MongoDb.v1.TimeOff;
using Domain.Enum.v1;
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

    public async Task<IEnumerable<TimeOffInformation>> FindAllAsync()
    {
        var collection = Database.GetCollection<TimeOffInformation>(_collection);

        var options = new FindOptions<TimeOffInformation>
        {
            Sort = Builders<TimeOffInformation>.Sort
                .Ascending(x => x.Status)
                .Ascending(x => x.CreatedAt)
        };

        var timeOffs = (await collection.FindAsync(Builders<TimeOffInformation>.Filter.Empty, options: options)).ToListAsync();

        return await timeOffs;
    }

    public async Task ApproveOrRejectAsync(
        string description,
        string approver,
        string protocol,
        bool isApprove)
    {
        var collection = Database.GetCollection<TimeOffInformation>(_collection);

        var filter = Builders<TimeOffInformation>.Filter.Eq(x => x.Protocol, protocol);

        var status = isApprove ? TimeOffEnum.Approved : TimeOffEnum.Rejected;

        var updateSet = Builders<TimeOffInformation>.Update
            .Set(x => x.Description, description)
            .Set(x => x.Approver, approver)
            .Set(x=> x.Status, status);

        await collection.UpdateOneAsync(filter, updateSet);
    }

    public async Task<IEnumerable<TimeOffInformation>> FindByEmailAsync(string email)
    {
        var collection = Database.GetCollection<TimeOffInformation>(_collection);

        var options = new FindOptions<TimeOffInformation>
        {
            Sort = Builders<TimeOffInformation>.Sort
                     .Ascending(x => x.Status)
                     .Ascending(x => x.CreatedAt)
        };

        var filter = Builders<TimeOffInformation>.Filter.Eq(x => x.UserEmail, email);

        var timeOffs = (await collection.FindAsync(filter, options: options)).ToListAsync();

        return await timeOffs;
    }

    public async Task RemoveByProtocol(string protocol)
    {
        var collection = Database.GetCollection<TimeOffInformation>(_collection);

        var filter = Builders<TimeOffInformation>.Filter.Eq(x => x.Protocol, protocol);
        await collection.DeleteOneAsync(filter);
    }
}

using Domain.Entities.MongoDb.v1.UserTimer;
using Domain.Interfaces.v1.Repositories.UserTimer;
using Infrastructure.Data.Mongo.Repositories.v1.Base;
using MongoDB.Driver;

namespace Infrastructure.Data.Mongo.Repositories.v1.UserTimer;

public class UserTimerRepository(
    string collectionName
    ) : MongoDbBaseRepository<UserTimerInformation>(collectionName), IUserTimerRepository
{
    private readonly string _collection = collectionName;
    public async Task InsertUserTimerAsync(
        UserTimerInformation userInformation)
    {
        var collection = Database.GetCollection<UserTimerInformation>(_collection);
        await collection.InsertOneAsync(userInformation);
    }

    public async Task UpdateUserTimerAsync(
        UserTimerInformation userInformation)
    {
        var collection = Database.GetCollection<UserTimerInformation>(_collection);

        var filter = Builders<UserTimerInformation>.Filter.Eq(x => x.Id, userInformation.Id);
        await collection.ReplaceOneAsync(filter, userInformation);
    }

    public async Task<UserTimerInformation> FindEmailAsync(
        string email)
    {
        var collection = Database.GetCollection<UserTimerInformation>(_collection);

        var filter = Builders<UserTimerInformation>.Filter.Eq(x => x.Email, email);

        var projection = Builders<UserTimerInformation>.Projection
            .Slice(x => x.Remarks, 5)
            .Include(x => x.Remarks)
            .Include(x => x.Email)
            .Include(x => x.Name)
            .Include(x => x.Hour);

        var result = await collection
            .Find(filter)
            .Project<UserTimerInformation>(projection)
            .FirstOrDefaultAsync();

        if (result?.Remarks != null)
            result.Remarks = [.. result.Remarks.OrderByDescending(r => r.UpdateAt).Take(5)];

        return result;
    }

    public async Task UpsertUserTimerAsync(
        UserTimerInformation userTimerInformation)
    {
        var collection = Database.GetCollection<UserTimerInformation>(_collection);

        var filter = Builders<UserTimerInformation>.Filter.Eq(x => x.Id, userTimerInformation.Id);

        var updateSet = Builders<UserTimerInformation>.Update
            .Set(x => x.Hour, userTimerInformation.Hour)
            .Set(x => x.Remarks, userTimerInformation.Remarks
        );

        await collection.UpdateOneAsync(filter, updateSet);
    }

    public async Task<IEnumerable<UserTimerInformation>> FindAllUserInformationAsync()
    {
        var collection = Database.GetCollection<UserTimerInformation>(_collection);

        var userTimers = await collection
            .Find(Builders<UserTimerInformation>.Filter.Empty)
            .ToListAsync();

        return userTimers;
    }
}
using Domain.Entities.MongoDb.v1.UserTimer;
using Domain.Interfaces.v1.Repositories.UserTimer;
using Infrastructure.Data.Mongo.Repositories.v1.Base;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Data.Mongo.Repositories.v1.UserTimer;

public class UserTimerRepository(
    string collectionName
    ) : MongoDbBaseRepository<UserTimerInformation>(collectionName), IUserTimerRepository
{
    private readonly string _collection = collectionName;
    public async Task AddAsync(
        UserTimerInformation userInformation)
    {
        var collection = Database.GetCollection<UserTimerInformation>(_collection);
        await collection.InsertOneAsync(userInformation);
    }

    public async Task UpdateAsync(
        UserTimerInformation userInformation)
    {
        var collection = Database.GetCollection<UserTimerInformation>(_collection);

        var filter = Builders<UserTimerInformation>.Filter.Eq(x => x.Id, userInformation.Id);
        await collection.ReplaceOneAsync(filter, userInformation);
    }

    public async Task<UserTimerInformation> FindByEmailAlternativeAsync(
        string emailAlternative)
    {
        var collection = Database.GetCollection<UserTimerInformation>(_collection);

        var filter = Builders<UserTimerInformation>.Filter.Eq(x => x.EmailAlternative, emailAlternative);

        var projection = Builders<UserTimerInformation>.Projection
            .Slice(x => x.Remarks, 5)
            .Include(x => x.Remarks)
            .Include(x => x.EmailAlternative)
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

    public async Task<UserTimerInformation> FindByEmailAsync(
        string email)
    {
        var collection = Database.GetCollection<UserTimerInformation>(_collection);

        var filter = Builders<UserTimerInformation>.Filter.Eq(x => x.Email, email);

        var result =(await collection
            .FindAsync(filter)).FirstOrDefaultAsync();

        return await result;
    }

    public async Task<UserTimerInformation> FindByIdAsync(
        ObjectId id)
    {
        var collection = Database.GetCollection<UserTimerInformation>(_collection);

        var filter = Builders<UserTimerInformation>.Filter.Eq(x => x.Id, id);

        var result =(await collection
            .FindAsync(filter)).FirstOrDefaultAsync();

        return await result;
    }

    public async Task UpsertAsync(
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

    public async Task<IEnumerable<UserTimerInformation>> FindAllAsync()
    {
        var collection = Database.GetCollection<UserTimerInformation>(_collection);

        var userTimers = await collection
            .Find(Builders<UserTimerInformation>.Filter.Empty)
            .ToListAsync();

        return userTimers;
    }

    public async Task CreateOrUpdateAsync(UserTimerInformation userTimerInformation)
    {
        var collection = Database.GetCollection<UserTimerInformation>(_collection);

        var filter = Builders<UserTimerInformation>.Filter.Eq(x => x.Id, userTimerInformation.Id);

        var existingUserTimer = await collection.Find(filter).FirstOrDefaultAsync();

        if (existingUserTimer != null)
        {
            await collection.ReplaceOneAsync(filter, userTimerInformation);
        }
        else
        {
            await collection.InsertOneAsync(userTimerInformation);
        }
    }
}
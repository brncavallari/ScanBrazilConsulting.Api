using Domain.Entities.MongoDb.v1.UserTimer;
using Domain.Interfaces.v1.UserTimer;
using Infrastructure.Data.Mongo.Repositories.v1.Base;
using MongoDB.Driver;

namespace Infrastructure.Data.Mongo.Repositories.v1.UserTimer;

public class UserTimerRepository(
    string collectionName
    ) : MongoDbBaseRepository<UserTimerInformation>(collectionName), IUserTimerRepository
{
    private readonly string _collection = collectionName;
    public async Task InsertUserTimerAsync(UserTimerInformation userInformation)
    {
        var collection = Database.GetCollection<UserTimerInformation>(_collection);
        await collection.InsertOneAsync(userInformation);
    }

    public async Task<UserTimerInformation> FindEmailAsync(string email)
    {
        var collection = Database.GetCollection<UserTimerInformation>(_collection);

        var filter = Builders<UserTimerInformation>.Filter.Eq(x => x.Email, email);

        var result = await collection.Find(filter).FirstOrDefaultAsync();

        return result;
    }

    public async Task UpsertUserTimerAsync(UserTimerInformation userTimerInformation)
    {
        var collection = Database.GetCollection<UserTimerInformation>(_collection);

        var filter = Builders<UserTimerInformation>.Filter.Eq(x => x.Email, userTimerInformation.Email);

        var updateSet = Builders<UserTimerInformation>.Update
            .Set(x => x.Hour, userTimerInformation.Hour)
            .Set(x=> x.Remarks, userTimerInformation.Remarks
        );

        await collection.UpdateOneAsync(filter, updateSet);
    }
}
using CrossCutting.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Data.Mongo.Repositories.v1.Base;
public class MongoDbBaseRepository<TEntity>
{
    [ExcludeFromCodeCoverage] protected IMongoDatabase Database { get; }
    private readonly string _collection;
    private readonly string _databaseName;
    private readonly MongoServerAddress _serverSettings;
    protected MongoDbBaseRepository(string collection) 
    {
        try 
        { 
            _collection = collection;
            _databaseName = AppSettings.Settings.ConnectionSettings.DatabaseName;
            RegisterClassMap();
            MongoClient mongoClient = new(AppSettings.Settings.ConnectionSettings.ConnectionString);
            Database = mongoClient.GetDatabase(_databaseName);
            _serverSettings = mongoClient.Settings.Server;
        } 
        catch (Exception ex)
        { 
            throw new Exception($"Error accessing Mongo Db Server. See inner Exception for more detail {ex.Message}");
        } 
    }

    private static void RegisterClassMap() 
    { 
        if (!BsonClassMap.IsClassMapRegistered(typeof(TEntity))) 
        { 
            BsonClassMap.RegisterClassMap(delegate (BsonClassMap<TEntity> classMap) 
            { 
                classMap.AutoMap();
                classMap.SetIgnoreExtraElements(ignoreExtraElements: true); });
        }
    }
}
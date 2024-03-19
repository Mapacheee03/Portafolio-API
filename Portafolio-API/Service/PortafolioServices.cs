using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using Portafolio_API.Models;
using Portafolio_API.Configurations;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using MongoDB.Bson;
using System.Text.Json;

namespace Portafolio_API.Services
{
    public class PortafolioServices
    {
        private readonly IMongoCollection<Portafolio_API.Models.Portafolio> _portafolioCollection;

        public PortafolioServices(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDB =
            mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _portafolioCollection =
                mongoDB.GetCollection<Portafolio>(databaseSettings.Value.CollectionName);
        }
        public async Task<List<Portafolio_API.Models.Portafolio>> GetAsync() =>
            await _portafolioCollection.Find(_ => true).ToListAsync();

        public async Task InsertPortafolio(Portafolio portafolioInsert)
        {
            await _portafolioCollection.InsertOneAsync(portafolioInsert);
        }

        public async Task DeletePortafolio(string portafolioId)
        {
            var filter = Builders<Portafolio>.Filter.Eq(s => s.Id, portafolioId);
            await _portafolioCollection.DeleteOneAsync(filter);
        }

        public async Task UpdatePortafolio(Portafolio dataToUpdate)
        {
            var filter = Builders<Portafolio>.Filter.Eq(s => s.Id, dataToUpdate.Id);
            await _portafolioCollection.ReplaceOneAsync(filter, dataToUpdate);
        }

        public async Task<Portafolio> GetPortafolioById(string idToSearch)
        {
            return await _portafolioCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(idToSearch) } }).Result.FirstAsync();
        }
    }
}
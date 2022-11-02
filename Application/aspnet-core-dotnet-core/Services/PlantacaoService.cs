using Green_eyes_server.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Green_Eyes_Back.Services
{
    public class PlantacaoService
    {
        private string ConnectionString = "mongodb+srv://greeneyes:yl3aZJcDM6kajlxz@cluster-greeneyes.aqhjiq6.mongodb.net/?retryWrites=true&w=majority";

        private readonly IMongoCollection<PlantacaoModel> _plantacoes;

        /*public PlantacaoService(IOptions<GreenEyesDatabaseSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);

            _plantacoes = (IMongoCollection<PlantacaoModel>)mongoClient
                .GetDatabase(options.Value.DatabaseName)
                .GetCollection<PlantacaoModel>(options.Value.CollectionName);
        }*/

        public PlantacaoService(string DatabaseName, string Collection)
        {
            var mongoClient = new MongoClient(ConnectionString);

            _plantacoes = (IMongoCollection<PlantacaoModel>)mongoClient
                .GetDatabase(DatabaseName)
                .GetCollection<PlantacaoModel>(Collection);
        }

        public async Task<PlantacaoModel> Get(string id) =>
            await _plantacoes.Find(s => s.id == id).FirstOrDefaultAsync();

        public async Task Create(PlantacaoModel student) =>
            await _plantacoes.InsertOneAsync(student);

        public async Task Update(string id, PlantacaoModel student) =>
            await _plantacoes.ReplaceOneAsync(s => s.id == id, student);

        public async Task Delete(string id) =>
            await _plantacoes.DeleteOneAsync(s => s.id == id);

        internal class GetAll
        {
            private string v1;
            private string v2;

            public GetAll(string v1, string v2)
            {
                this.v1 = v1;
                this.v2 = v2;
            }
        }
    }
}

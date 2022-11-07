using MongoDB.Driver;

namespace aspnet_core_dotnet_core.Services
{
    public class MongoDBConn
    {
        private string _user = "greeneyes";
        private string _password = "1234567890";
        private string _database = "greenEyes_db";
        public MongoClient GetConexao()
        {
            MongoClient client = new MongoClient($"mongodb+srv://{_user}:{_password}@cluster-greeneyes.aqhjiq6.mongodb.net/?retryWrites=true&w=majority");
            return client;
        }

        public IMongoDatabase GetDatabase(MongoClient client)
        {
            return client.GetDatabase(_database);
        }


    }
}

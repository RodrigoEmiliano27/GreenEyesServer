using Green_eyes_server.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace aspnet_core_dotnet_core.Services
{
    public abstract class PadraoServiceMongo<T> where T : PadraoModel
    {

        protected PadraoServiceMongo()
        {
            SetCollection();
        }


        protected abstract void SetCollection();

        protected string Collection { get; set; }
        protected string SearchStringKey { get; set; }


        public abstract T FindByString(string value);


        public virtual void Insert(T model)
        {
            var collection = Connect();

            collection.InsertOne(model);

        }

        protected IMongoCollection<T> Connect()
        {
            MongoDBConn conexao = new MongoDBConn();

            return conexao.GetDatabase(conexao.GetConexao()).GetCollection<T>(Collection);
        }

        public virtual void Update(T model)
        {
            var collection = Connect();

            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", model.id);

            UpdateDefinition<T> update = UpdateFields(model);

            collection.UpdateOne(filter, update);
        }
        public abstract UpdateDefinition<T> UpdateFields(T model);
    

        public virtual void Delete(int id)
        {
            /* var p = new SqlParameter[]
            {
                 new SqlParameter("id", id),
                 new SqlParameter("tabela", Tabela)
            };
            HelperDAO.ExecutaProc("spDelete", p);*/
        }

        public virtual T Consulta(ObjectId id)
        {
            var collection = Connect();

            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);

            return collection.Find<T>(filter).First();
        }

        public virtual void Desabilitar(ObjectId id)
        {
            var collection = Connect();
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);
            UpdateDefinition<T> update = Builders<T>.Update.Set(x => x.Ativado, false);
            collection.UpdateOne(filter, update);
        }

        public virtual void Habilitar(ObjectId id)
        {
            var collection = Connect();
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);
            UpdateDefinition<T> update = Builders<T>.Update.Set(x => x.Ativado, true);
            collection.UpdateOne(filter, update);
        }

        public virtual List<T> Listagem()
        {
            /*var p = new SqlParameter[]
            {
                new SqlParameter("tabela", Tabela),
                new SqlParameter("Ordem", "1") // 1 é o primeiro campo da tabela,
                                // ou seja, a chave primária
            };
            var tabela = HelperDAO.ExecutaProcSelect(NomeSpListagem, p);
            List<T> lista = new List<T>();
            foreach (DataRow registro in tabela.Rows)
            {
                lista.Add(MontaModel(registro));
            }*/
            return null;
        }






    }
}

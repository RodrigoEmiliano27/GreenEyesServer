using aspnet_core_dotnet_core.Services;
using Green_eyes_server.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Green_Eyes_Back.Services
{
    public class UsuarioService : PadraoServiceMongo<UserModel>
    {
        public override UserModel FindByString(string value)
        {
         
           var betterFilter = Builders<UserModel>.Filter.Eq(a => a.Login, value);
            var collection = Connect();
            var foundItens = collection.Find<UserModel>(betterFilter).FirstOrDefault();
            return foundItens;
        }

        public  UserModel FindByLoginSenha(UserModel model)
        {
            var filterBuilder = Builders<UserModel>.Filter;
            var filter = filterBuilder.Eq(a => a.Ativado, true) &
                filterBuilder.Eq(x => x.Login, model.Login) &
                filterBuilder.Eq(x => x.Senha, model.Senha);

            var collection = Connect();
            var foundItens = collection.Find<UserModel>(filter).FirstOrDefault();
            return foundItens;
        }
        public List<UserModel> GetUsersList(ObjectId idPlant)
        {
            var filterBuilder = Builders<UserModel>.Filter;
            var filter = filterBuilder.Eq(a => a.id_plantacao, idPlant);

            var collection = Connect();
            var foundItens = collection.Find<UserModel>(filter).ToList();

            return foundItens;

        }
        public UserModel FindById(ObjectId value)
        {
            var betterFilter = Builders<UserModel>.Filter.Eq(a => a.id, value);
            var collection = Connect();
            var foundItens = collection.Find<UserModel>(betterFilter).FirstOrDefault();
            return foundItens;
        }

        protected override void SetCollection()
        {
            this.Collection = "usuarios";
            this.SearchStringKey = "Login";
        }

        public virtual void UpdatePassword(ObjectId id, string senha)
        {
            var collection = Connect();
            FilterDefinition<UserModel> filter = Builders<UserModel>.Filter.Eq("_id", id);
            UpdateDefinition<UserModel> update = UpdatePasswordDefinition(senha);
            collection.UpdateOne(filter, update);
        }

        private UpdateDefinition<UserModel> UpdatePasswordDefinition(string senha)
        {
            var update = Builders<UserModel>.Update.Set(x => x.Senha, senha);
            return update;
        }

        public override UpdateDefinition<UserModel> UpdateFields(UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}
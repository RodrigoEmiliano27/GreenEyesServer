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

        public override UpdateDefinition<UserModel> UpdateFields(UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}
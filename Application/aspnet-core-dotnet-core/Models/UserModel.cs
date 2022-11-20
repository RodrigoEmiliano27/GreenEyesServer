using aspnet_core_dotnet_core.Models;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Green_eyes_server.Model
{
    public class UserModel:UserModelBasico
    {
        public UserModel()
        {
            //this.GenerateID();
        }
        [BsonElement("Senha")]
        public string Senha { get; set; }

        public UserModel(UserModelCreateViewModel createm)
        {
            this.Login = createm.Login;
            this.Nome = createm.Nome;
            this.Senha = createm.Senha;
            this.Tipo = int.Parse(createm.TipoString);
            this.id_plantacao = createm.id_plantacao;
        }

    }
}

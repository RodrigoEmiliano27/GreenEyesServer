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

    }
}

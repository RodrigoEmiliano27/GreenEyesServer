using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Green_eyes_server.Model
{
    public class UserModelBasico:PadraoModel
    {
        public UserModelBasico()
        {
            //this.GenerateID();
        }
        [BsonElement("Nome")]
        public string Nome { get; set; }
        [BsonElement("Login")]
        public string Login { get; set; }
        [BsonElement("Tipo")]
        public int Tipo { get; set; }
        public ObjectId id_plantacao { get; set; }
    }
}

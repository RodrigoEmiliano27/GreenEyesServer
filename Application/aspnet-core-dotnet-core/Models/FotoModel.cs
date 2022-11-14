using MongoDB.Bson;
using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Green_eyes_server.Model
{
    public class FotoModel: PadraoModel
    {
        public FotoModel()
        {
            //this.GenerateID();
        }

        [BsonElement("Nome")]
        public string Nome { get; set; }

        [BsonElement("Tipo")]
        public string Tipo { get; set; }

        [BsonElement("Data")]
        public DateTime Data { get; set; }

        [BsonElement("Usuario")]
        public ObjectId Id_usuario { get; set; }

        [BsonElement("Plantacao")]
        public ObjectId Id_plantacao { get; set; }

        [BsonElement("Classificado")]
        public bool Classificado { get; set; } = false;

        [BsonElement("Segmentado")]
        public bool Segmentado { get; set; } = false;








    }
}

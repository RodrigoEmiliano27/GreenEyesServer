using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Green_eyes_server.Model
{
    public class PadraoModel
    {
        public PadraoModel()
        {
            //GenerateID();
        }

        [BsonElement("_id")]
        public virtual ObjectId id { get; set; } =ObjectId.GenerateNewId();

        [BsonElement("ativado")]
        public virtual bool Ativado { get; set; } = true;

        
    }
}

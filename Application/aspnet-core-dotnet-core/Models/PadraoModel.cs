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

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string id { get; set; }

        [BsonElement("ativado")]
        public virtual bool Ativado { get; set; } = true;

        public void GenerateID()
        {
            byte[] randomByte = new byte[16];

            new Random().NextBytes(randomByte);

            Guid mGuid = new Guid(randomByte);

            id = mGuid.ToString();
        }
    }
}

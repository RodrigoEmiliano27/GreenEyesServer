using MongoDB.Bson.Serialization.Attributes;

namespace Green_eyes_server.Model
{
    public class PlantacaoModel:PadraoModel
    {
        public PlantacaoModel()
        {
            //this.GenerateID();
        }

        [BsonElement("plant_name")]
        public string plantacao { get; set; }
    }
}

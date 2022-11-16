using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Green_eyes_server.Model
{
    public class DiagnosticoModel:PadraoModel
    {
        public DiagnosticoModel()
        {
            //this.GenerateID();
        }
        [BsonElement("Id_foto_segmentada")]
        public ObjectId Id_foto_segmentada { get; set; }

        [BsonElement("Id_plantacao")]
        public ObjectId Id_plantacao { get; set; }

        [BsonElement("Id_foto")]
        public ObjectId Id_Foto { get; set; }

        [BsonElement("Data")]
        public DateTime Data { get; set; }

        [BsonElement("Azure_Custom_Vision")]
        public ImagePrediction Classificacao_Azure { get; set; }

        [BsonElement("Sem_classificacao")]
        public bool SemClassificacao { get; set; } = false;

        [BsonElement("Certeza")]
        public double Grau_certeza { get; set; }

        [BsonElement("Classificacao_final")]
        public string Classificacao_final { get; set; }


    }
}

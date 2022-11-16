using Green_eyes_server.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace aspnet_core_dotnet_core.Services
{
    public class DiagnosticoService : PadraoServiceMongo<DiagnosticoModel>
    {
        public override DiagnosticoModel FindByString(string value)
        {
            throw new System.NotImplementedException();
        }

        public override UpdateDefinition<DiagnosticoModel> UpdateFields(DiagnosticoModel model)
        {
            var update = Builders<DiagnosticoModel>.Update.Set(x => x.Classificacao_final, "teste")
            .Set(x => x.Grau_certeza, 0.85);
            return update;
        }

        protected override void SetCollection()
        {
            Collection = "diagnostico";
        }
    }
}

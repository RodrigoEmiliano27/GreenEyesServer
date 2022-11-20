using aspnet_core_dotnet_core.Models;
using Green_eyes_server.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aspnet_core_dotnet_core.Services
{
    public class DiagnosticoService : PadraoServiceMongo<DiagnosticoModel>
    {
        public override DiagnosticoModel FindByString(string value)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<DiagnosticoViewModel>> DiagnosticoDates(DateTime start, DateTime end, ObjectId idPlantacao)
        {
            List<DiagnosticoViewModel> Itens = new List<DiagnosticoViewModel>();
            List<DiagnosticoModel> lista = FindByDates(start, end, idPlantacao);

            if (lista != null)
            {
                FotoService fotoService= new FotoService();

                foreach (DiagnosticoModel diag in lista)
                {

                    if (diag.Id_plantacao == idPlantacao)
                    {
                        DiagnosticoViewModel diagnosticoModel = new DiagnosticoViewModel();
                        diagnosticoModel.Diagnostico = diag;
                        FotoModel foto = fotoService.Consulta(diag.Id_Foto);
                        byte[] RawData = await AzureStorageHelper.GetDataFromBlob($"plant-{foto.Id_plantacao.ToString()}", foto.Nome);
                        string Imagebase64 = Convert.ToBase64String(RawData);
                        string image = $"data:image/{foto.Tipo};base64,{Imagebase64}";
                        diagnosticoModel.Imagem = image;
                        Itens.Add(diagnosticoModel);


                    }

                }
                return Itens;
            }
            return null;

        }

        public List<DiagnosticoModel> FindByDates(DateTime start, DateTime end, ObjectId id)
        {
            var filterBuilder = Builders<DiagnosticoModel>.Filter;
            var filter = filterBuilder.Gte(x => x.Data, new BsonDateTime(start)) &
             filterBuilder.Lte(x => x.Data, new BsonDateTime(end)) &
             filterBuilder.Eq(x => x.Id_plantacao, id);

            var collection = Connect();
            var itens = collection.Find(filter).ToList();


            return itens;

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

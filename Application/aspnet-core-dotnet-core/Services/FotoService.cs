using aspnet_core_dotnet_core.Models;
using Green_eyes_server.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aspnet_core_dotnet_core.Services
{
    public class FotoService : PadraoServiceMongo<FotoModel>
    {
        public override FotoModel FindByString(string value)
        {
            throw new System.NotImplementedException();
        }

        public FotoModel FindByNotPredicted()
        {
            var betterFilter = Builders<FotoModel>.Filter.Eq(a => a.Classificado, false);
            var collection = Connect();
            var foundItens = collection.Find<FotoModel>(betterFilter).FirstOrDefault();
            return foundItens;
        }

        public List<FotoModel> FindByMonthYear(int month, int year)
        {
            string jsonquery = "{Data:{$gte:ISODate('dataInicial'),$lt:ISODate('datafinal')}}";
            if (month == 12)
            {
                jsonquery = jsonquery.Replace("dataInicial", $"{year.ToString()}-{month.ToString("00")}-01");
                jsonquery = jsonquery.Replace("datafinal", $"{(year+1).ToString()}-01-01");
            }
            else
            {
                jsonquery = jsonquery.Replace("dataInicial", $"{year.ToString()}-{month.ToString("00")}-01");
                jsonquery = jsonquery.Replace("datafinal", $"{year.ToString()}-{(month+1).ToString("00")}-01");
            }

            BsonDocument doc = MongoDB.Bson.Serialization
                   .BsonSerializer.Deserialize<BsonDocument>(jsonquery);


            var collection = Connect();
            var itens = collection.Find(doc).ToList();

            return itens;

        }

        public List<FotoModel> FindByDates(DateTime start, DateTime end,ObjectId id)
        {
            var filterBuilder = Builders<FotoModel>.Filter;
            var filter = filterBuilder.Gte(x => x.Data, new BsonDateTime(start)) &
             filterBuilder.Lte(x => x.Data, new BsonDateTime(end)) &
             filterBuilder.Eq(x => x.Id_plantacao, id);

            var collection = Connect();
            var itens = collection.Find(filter).ToList(); 

            
            return itens;

        }
        public async Task<List<string>> FotosMesAno(int month, int year)
        {
            List<string> imagens = new List<string>();
            List<FotoModel> lista = FindByMonthYear(month, year);
            if (lista != null)
            {
                foreach (FotoModel foto in lista)
                {
                    byte[] RawData=await AzureStorageHelper.GetDataFromBlob($"plant-{foto.Id_plantacao.ToString()}", foto.Nome);
                    string Imagebase64 = Convert.ToBase64String(RawData);
                    string image = $"data:image/{foto.Tipo};base64,{Imagebase64}";
                    imagens.Add(image);

                }
                return imagens;
            }
            return null;
            
        }

        public async Task<List<FotosViewModel>> FotosDates(DateTime start, DateTime end, ObjectId idPlantacao)
        {
            List<FotosViewModel> fotos = new List<FotosViewModel>();
            List<FotoModel> lista = FindByDates(start, end, idPlantacao);
            if (lista != null)
            {
                foreach (FotoModel foto in lista)
                {
                    if (foto.Id_plantacao == idPlantacao)
                    {
                        FotosViewModel fotoView = new FotosViewModel();
                        byte[] RawData = await AzureStorageHelper.GetDataFromBlob($"plant-{foto.Id_plantacao.ToString()}", foto.Nome);
                        string Imagebase64 = Convert.ToBase64String(RawData);
                        string image = $"data:image/{foto.Tipo};base64,{Imagebase64}";
                        fotoView.imagem = image;
                        fotoView.foto = foto;
                        fotos.Add(fotoView);
                    }                

                }
                return fotos;
            }
            return null;

        }
        protected override void SetCollection()
        {
            this.Collection = "fotos";
        }

        public override UpdateDefinition<FotoModel> UpdateFields(FotoModel model)
        {
            var update = Builders<FotoModel>.Update.Set(x => x.Classificado, model.Classificado)
           .Set(x => x.Nome, model.Nome)
           .Set(x => x.Segmentado, model.Segmentado);
            return update;
        }
    }
}

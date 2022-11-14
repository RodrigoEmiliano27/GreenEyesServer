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

        protected override void SetCollection()
        {
            this.Collection = "fotos";
        }
    }
}

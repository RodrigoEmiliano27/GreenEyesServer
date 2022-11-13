using aspnet_core_dotnet_core.Services;
using Green_eyes_server.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Green_Eyes_Back.Services
{
    public class PlantacaoService : PadraoServiceMongo<PlantacaoModel>
    {
        public override PlantacaoModel FindByString(string value)
        {
            throw new NotImplementedException();
        }

        protected override void SetCollection()
        {
            this.Collection = "plantacao";
            
        }

        
    }
}

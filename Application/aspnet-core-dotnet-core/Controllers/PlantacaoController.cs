using aspnet_core_dotnet_core.Controllers;
using Green_Eyes_Back.Services;
using Green_eyes_server.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Green_Eyes_Back.Controllers
{
    public class PlantacaoController : PadraoController<PlantacaoModel>
    {

        public PlantacaoController()
        {
            this.ExibeAutenticacao = false;
            this.DAO = new PlantacaoService();
        }

        protected override void ValidaDados(PlantacaoModel model, string operacao)
        {
            ModelState.Clear();
            if (model.plantacao == null || model.plantacao == "")
                ModelState.AddModelError("plantacao", "Nome de plantação inválido!");

        }

    }
}

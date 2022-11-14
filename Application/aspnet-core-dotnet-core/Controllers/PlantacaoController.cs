using aspnet_core_dotnet_core.Controllers;
using Green_Eyes_Back.Models;
using Green_Eyes_Back.Services;
using Green_eyes_server.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnet_core_dotnet_core.Services;

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
        public override IActionResult Save(PlantacaoModel model, string Operacao)
        {
            try
            {
                ValidaDados(model, Operacao);
                if (ModelState.IsValid == false)
                {
                    ViewBag.Operacao = Operacao;
                    PreencheDadosParaView(Operacao, model);
                    return View(NomeViewForm, model);
                }
                else
                {
                    if (Operacao == "I")
                        SaveData(model);

                    /*else
                        DAO.Update(model);*/
                    return RedirectToAction(NomeViewIndex);
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        private async void SaveData(PlantacaoModel model)
        {
            DAO.Insert(model);
            UserModel usuario = new UserModel();
            usuario.id_plantacao = model.id;
            usuario.Nome = $"{model.plantacao}-admin";
            usuario.Tipo = 1;
            usuario.Login = usuario.Nome;
            usuario.Senha = usuario.Nome;
            UsuarioService service = new UsuarioService();
            service.Insert(usuario);
            await AzureStorageHelper.CreateContainerAsync($"plant-{model.id.ToString()}");

        }


    }
}

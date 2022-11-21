using aspnet_core_dotnet_core.Controllers;
using aspnet_core_dotnet_core.Models;
using Green_Eyes_Back.Models;
using Green_Eyes_Back.Services;
using Green_eyes_server.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Green_Eyes_Back.Controllers
{
    public class UserController:Controller
    {
        public UserController()
        {
        }

        public virtual IActionResult Index()
        {
            try
            {
                var lista = new UsuarioService().GetUsersList(new MongoDB.Bson.ObjectId(HttpContext.Session.GetString("idPlant")));
                return View("Index", lista);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public virtual IActionResult DesabilitaAcesso(string id)
        {
            try
            {
                UsuarioService service = new UsuarioService();
                service.Desabilitar(new MongoDB.Bson.ObjectId(id));
                return Index();
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public virtual IActionResult HabilitaAcesso(string id)
        {
            try
            {
                UsuarioService service = new UsuarioService();
                service.Habilitar(new MongoDB.Bson.ObjectId(id));
                return Index();
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.Tipo = HttpContext.Session.GetString("Tipo");
            ViewBag.Logado = HttpContext.Session.GetString("Logado");
            if (!HelperControllers.VerificaUserLogado(HttpContext.Session) && context.RouteData.Values["action"].ToString() == "Index" && context.RouteData.Values["controller"].ToString() == "Usuario")
            {
                context.Result = RedirectToAction("Index", "Login");
            }


            if (!HelperControllers.VerificaUserLogado(HttpContext.Session))
                context.Result = RedirectToAction("Index", "Login");
            else
            {
                ViewBag.Logado = true;
                base.OnActionExecuting(context);
            }
        }

        public virtual IActionResult Create()
        {
            try
            {
                ViewBag.Operacao = "I";
                UserModelCreateViewModel model = new UserModelCreateViewModel();
                PrepararTiposUsuario();
                //PreencheDadosParaView("I", model);
                return View("Form", model);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        protected virtual void ValidaDados(UserModelCreateViewModel model, string operacao)
        {
            ModelState.Clear();
            if(model.Nome==null || model.Nome=="")
                ModelState.AddModelError("Nome", "Nome não pode ser vazio!");
            if (model.Login == null || model.Login == "")
                ModelState.AddModelError("Login", "Login não pode ser vazio!");
            if (model.Senha == null || model.Senha == "")
                ModelState.AddModelError("Senha", "Senha inválida!");
            if (model.SenhaRepetida != model.Senha )
                ModelState.AddModelError("Senha", "Senhas não se repetem!");

        }

        protected void PrepararTiposUsuario()
        {
           
            List<SelectListItem> listaCategorias = new List<SelectListItem>();
            SelectListItem item = new SelectListItem("Operador", "2");
            SelectListItem item2 = new SelectListItem("Administrador", "1");
            listaCategorias.Add(item);
            listaCategorias.Add(item2);
            ViewBag.Categorias = listaCategorias;
        }

        public virtual IActionResult Save(UserModelCreateViewModel model, string Operacao)
        {
            try
            {
                ValidaDados(model, Operacao);
                if (ModelState.IsValid == false)
                {
                    PrepararTiposUsuario();
                    ViewBag.Operacao = Operacao;
                    //PreencheDadosParaView(Operacao, model);
                    return View("Form", model);
                }
                else
                {
                    if (Operacao == "I")
                    {
                        UsuarioService service = new UsuarioService();
                        model.id_plantacao = new MongoDB.Bson.ObjectId(HttpContext.Session.GetString("idPlant"));
                        UserModel usuario = new UserModel(model);
                        service.Insert(usuario);
                    }
                        
                    /*else
                        DAO.Update(model);*/
                    return RedirectToAction("Index","Home");
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult AlterarSenha()
        {
            return View("AltSenha", new UserPasswordChangeViewModel());
        }

        protected void ValidaSenha(UserPasswordChangeViewModel senha)
        {
            ModelState.Clear();
            if(senha.Senha!=senha.Senha_repetida)
                ModelState.AddModelError("Senha", "Senhas não se repetem!");
        }

        public IActionResult SalvarSenhaAlterada(UserPasswordChangeViewModel ModelSenha)
        {
            try
            {
                ValidaSenha(ModelSenha);
                if (ModelState.IsValid == false)
                {
                    return View("AltSenha", ModelSenha);
                }
                else
                {
                    UsuarioService service = new UsuarioService();
                    service.UpdatePassword(new MongoDB.Bson.ObjectId(HttpContext.Session.GetString("Logado")), ModelSenha.Senha);
                    return RedirectToAction("Index","Home");
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

         
    }
}

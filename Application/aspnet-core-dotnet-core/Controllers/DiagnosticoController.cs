using aspnet_core_dotnet_core.Controllers;
using aspnet_core_dotnet_core.Models;
using aspnet_core_dotnet_core.Services;
using Green_Eyes_Back.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Green_Eyes_Back.Controllers
{
    public class DiagnosticoController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                ViewBag.Tipo = null;
                return View("Index");
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

            if (!HelperControllers.VerificaUserLogado(HttpContext.Session))
                context.Result = RedirectToAction("Index", "Login");
            else
            {
                ViewBag.Logado = true;
                base.OnActionExecuting(context);
            }
        }

        public async Task<IActionResult> GetDatesAsync()
        {
            try
            {
                string start = String.Format("{0}", Request.Form["datestart"]);
                string end = String.Format("{0}", Request.Form["dateend"]);
                DateTime startdate = DateTime.Parse(start);
                DateTime enddate = DateTime.Parse(end);
                DiagnosticoService service = new DiagnosticoService();
                ObjectId plant = ObjectId.Parse(HttpContext.Session.GetString("idPlant"));
                List<DiagnosticoViewModel> lista = await service.DiagnosticoDates(startdate, enddate, plant);
                if (lista.Count > 0)
                {
                    return View("See", lista);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }


        }




    }
}


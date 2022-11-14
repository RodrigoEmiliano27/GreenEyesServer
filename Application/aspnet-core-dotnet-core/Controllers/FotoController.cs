using aspnet_core_dotnet_core.Models;
using Green_eyes_server.Model;
using Green_Eyes_Back.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnet_core_dotnet_core.Services;
using aspnet_core_dotnet_core.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Green_Eyes_Back.Controllers
{
    public class FotoController : Controller
    {
        
        public IActionResult Index()
        {
            ViewBag.Tipo = null;
            return View("Index");
        }

        public IActionResult SeeImages(List_Images lista)
        {
            ViewBag.Tipo = null;
            return View("See", lista);
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
            string start = String.Format("{0}", Request.Form["datestart"]);
            string end = String.Format("{0}", Request.Form["dateend"]);
            DateTime startdate = DateTime.Parse(start);
            DateTime enddate = DateTime.Parse(end);
            FotoService service = new FotoService();
            List<string> lista = await service.FotosDates(startdate, enddate);
            if (lista.Count > 0)
            {
                List_Images listImages = new List_Images();
                listImages.Images = lista;
                return View("See", listImages);
            }
            return RedirectToAction("Index", "Home");

        }




    }
}

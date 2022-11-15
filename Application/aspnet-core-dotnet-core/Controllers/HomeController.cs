using Green_Eyes_Back.Models;
using Green_Eyes_Back.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Green_eyes_server.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using aspnet_core_dotnet_core.Services;
using aspnet_core_dotnet_core.Models;
using aspnet_core_dotnet_core.Services;

namespace Green_Eyes_Back.Controllers
{
    public class HomeController : Controller
    {

        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async void Teste()
        {
            /*
             await AzureStorageHelper.CreateContainerAsync("plant-636869d7782d0d7d53ea13d2");*/

            VisionAPIService.SendImageToApi();
            //service.FindByMonthYear(11, 2022);
            //await AzureStorageHelper.GetImageFromAzure("plant-6369a24fb23893744f69060c", "imagesteste1");
        }

        [HttpGet]
        [Authorize(Roles = "1,2")]
        public string Teste3()
        {
           
            return "teste";
        }

        [HttpPut]
        public string Teste2()
        {
            MongoClient client = new MongoClient("mongodb+srv://greeneyes:1234567890@cluster-greeneyes.aqhjiq6.mongodb.net/?retryWrites=true&w=majority");

            //List<string> databases = client.ListDatabaseNames().ToList();

            var plantCollection = client.GetDatabase("greenEyes_db").GetCollection<PlantacaoModel>("plantacao");

            PlantacaoModel plant = new PlantacaoModel();
            plant.plantacao = "teste1";

            plantCollection.InsertOne(plant);

            return "teste";
        }
    }
}

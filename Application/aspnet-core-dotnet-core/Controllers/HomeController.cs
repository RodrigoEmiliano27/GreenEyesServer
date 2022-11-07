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
        public void Teste()
        {
            PlantacaoModel platn = new PlantacaoModel();
            platn.plantacao = "teste";

            new PlantacaoService("greenEyes_db","plantacao").Create(platn);
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

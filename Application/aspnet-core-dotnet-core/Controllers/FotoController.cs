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

namespace Green_Eyes_Back.Controllers
{
    public class FotoController : Controller
    {
        [HttpPost]
        [Route("SavePhoto")]
        [Authorize(Roles = "1")]   
        public async Task<ActionResult<dynamic>> SavePhoto([FromBody] ImageAPI model)
        {
            string id= User.Identity.Name;
            UsuarioService ServiceUser = new UsuarioService();
            UserModel user=ServiceUser.FindById(MongoDB.Bson.ObjectId.Parse(id));
            if (user != null)
            {
                FotoModel foto = new FotoModel();
                foto.Id_plantacao = user.id_plantacao;
                foto.Id_usuario = user.id;
                foto.Data = DateTime.Now.ToUniversalTime();
                foto.Nome = model.Nome;
                foto.Tipo = model.Tipo;

                FotoService fotoService = new FotoService();
                fotoService.Insert(foto);

                AzureStorageHelper.UploadToAzure($"plant-{foto.Id_plantacao.ToString()}", foto.Nome, model.Image);

            }
            else
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}

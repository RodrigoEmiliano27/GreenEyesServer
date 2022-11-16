using aspnet_core_dotnet_core.Models;
using aspnet_core_dotnet_core.Services;
using Green_Eyes_Back.Models;
using Green_Eyes_Back.Services;
using Green_eyes_server.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_core_dotnet_core.Controllers
{
    public class ApIController : ControllerBase
    {
        [HttpPost]
        [Route("SavePhoto")]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<dynamic>> SavePhoto([FromBody] ImageAPI model)
        {
            try
            {
                string id = User.Identity.Name;
                UsuarioService ServiceUser = new UsuarioService();
                UserModel user = ServiceUser.FindById(MongoDB.Bson.ObjectId.Parse(id));
                if (user != null)
                {
                    FotoModel foto = new FotoModel();
                    foto.Id_plantacao = user.id_plantacao;
                    foto.Id_usuario = user.id;
                    foto.Data = DateTime.Now.ToUniversalTime();
                    foto.Nome = model.Nome;
                    foto.Tipo = model.Tipo;

                    AzureStorageHelper.UploadToAzure($"plant-{foto.Id_plantacao.ToString()}", foto.Nome, model.Image);

                    FotoService fotoService = new FotoService();
                    fotoService.Insert(foto);



                }
                else
                {
                    throw new Exception("Usuário inválido!");
                }

                return Ok();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.ToString());

            }
        }
    }
}

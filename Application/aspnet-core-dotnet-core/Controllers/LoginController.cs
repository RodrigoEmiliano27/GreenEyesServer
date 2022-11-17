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

namespace Green_Eyes_Back.Controllers
{
    public class LoginController:Controller
    {
        public IActionResult Index()
        {
            ViewBag.Tipo = null;
            return View("Index", new UserModel());
        }


        private UserModel GetUser(UserModel model)
        {
            // Recupera o usuário
            UsuarioService service = new UsuarioService();
            UserModel db_user = service.FindByString(model.Login);

            // Verifica se o usuário existe e é válido
            if (!(db_user != null && db_user.Login == model.Login && db_user.Senha == model.Senha))
                return null;
            else
                return db_user;
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ActionResult<dynamic>> FazLogin(UserModel model)
        {
            try
            {
                UserModel db_user = GetUser(model);
                if (db_user == null)
                {
                    ModelState.AddModelError("Login", "Login ou senha inválidos!");
                    ModelState.AddModelError("Senha", "Login ou senha inválidos!");
                    return View("index", new UserModel());
                }
                // Oculta a senha
                db_user.Senha = "";
                // Gera o Token
                var token = TokenService.GenerateToken(db_user);

                HttpContext.Session.SetString("Logado", db_user.id.ToString());
                HttpContext.Session.SetString("idPlant", db_user.id_plantacao.ToString());
                HttpContext.Session.SetString("Name", db_user.Nome);
                HttpContext.Session.SetString("Tipo", db_user.Tipo.ToString());
                HttpContext.Session.SetString("Token", token);
                return RedirectToAction("index", "Home");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }

        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UserModel model)
        {
            try
            {
                UserModel db_user = GetUser(model);
                if (db_user == null)
                    return NotFound(new { message = "Usuário ou senha inválidos" });

                // Gera o Token
                var token = TokenService.GenerateToken(db_user);

                // Oculta a senha
                db_user.Senha = "";

                // Retorna os dados
                return new
                {
                    user = db_user,
                    token = token
                };
            }
            catch(Exception erro)
            {
                return BadRequest(erro.ToString());
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index", "Home");
        }

        
        }

    

}

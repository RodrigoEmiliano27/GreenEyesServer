using Green_Eyes_Back.Services;
using Green_eyes_server.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Green_Eyes_Back.Controllers
{
    public class LoginController:Controller
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UserModel model)
        {
            // Recupera o usuário
            var user = model;

            user.Nome = "Rodrigo";
            user.Tipo = 1;

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenService.GenerateToken(user);

            // Oculta a senha
            user.Senha = "";

            // Retorna os dados
            return new
            {
                user = user,
                token = token
            };
        }

    }
}

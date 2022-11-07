using Microsoft.AspNetCore.Http;
using System;

namespace aspnet_core_dotnet_core.Controllers
{
    public class HelperControllers
    {
        public static Boolean VerificaUserLogado(ISession session)
        {
            string logado = session.GetString("Logado");
            if (logado == null)
                return false;
            else
                return true;
        }
        public static int GetUserLogadoID(ISession session)
        {
            string logado = session.GetString("Logado");
            if (logado == null)
                return -1;
            else
                return Convert.ToInt32(logado);
        }
    }
}

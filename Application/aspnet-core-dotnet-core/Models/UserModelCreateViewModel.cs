using Green_eyes_server.Model;

namespace aspnet_core_dotnet_core.Models
{
    public class UserModelCreateViewModel:UserModel
    {
        public string SenhaRepetida { get; set; }

        public string TipoString { get; set; }
    }
}

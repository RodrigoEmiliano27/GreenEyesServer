using System.Text.Json.Serialization;

namespace Green_eyes_server.Model
{
    public class UserModel:UserModelBasico
    {
        public UserModel()
        {
            this.GenerateID();
        }
        public string Senha { get; set; }

    }
}

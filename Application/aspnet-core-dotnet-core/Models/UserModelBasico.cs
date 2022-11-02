namespace Green_eyes_server.Model
{
    public class UserModelBasico:PadraoModel
    {
        public UserModelBasico()
        {
            this.GenerateID();
        }
        public string Nome { get; set; }
        public string Login { get; set; }
        public int Tipo { get; set; }
        public int id_plantacao { get; set; }
    }
}

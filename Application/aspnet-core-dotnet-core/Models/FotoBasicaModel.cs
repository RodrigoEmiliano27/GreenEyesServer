namespace Green_eyes_server.Model
{
    public class FotoBasicaModel:PadraoModel
    {
        public FotoBasicaModel()
        {
            this.GenerateID();
        }
        public string data { get; set; }

        public string id_usuario { get; set; }

        public string FotoEmBase64 { get; set; }
    }
}

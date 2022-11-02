namespace Green_eyes_server.Model
{
    public class DiagnosticoModel:PadraoModel
    {
        public DiagnosticoModel()
        {
            this.GenerateID();
        }

        public string id_foto_segmentada { get; set; }
        public string data { get; set; }
        public string classificacao_Azure { get; set; }
        public double grau_certeza { get; set; }
        public string classificacao_final { get; set; }


    }
}

namespace Green_eyes_server.Model
{
    public class FotoSegmentadaModel:PadraoModel
    {
        public FotoSegmentadaModel()
        {
            this.GenerateID();
        }
        public string data { get; set; }

        public string id_foto { get; set; }

        public string link { get; set; }

        public string versao_segmentador { get; set; }


    }
}

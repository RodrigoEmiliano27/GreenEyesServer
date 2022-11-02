using System;

namespace Green_eyes_server.Model
{
    public class FotoModel: PadraoModel
    {
        public FotoModel()
        {
            this.GenerateID();
        }
        public string link { get; set; }

        public DateTime data { get; set; }

        public string id_usuario { get; set; }








    }
}

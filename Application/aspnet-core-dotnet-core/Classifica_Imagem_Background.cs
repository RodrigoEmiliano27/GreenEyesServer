using aspnet_core_dotnet_core.Services;
using Green_eyes_server.Model;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace aspnet_core_dotnet_core
{
    public class Classifica_Imagem_Background : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //tenta enviar a imagem para ser classificada.
                try
                {
                    FotoService foto_service = new FotoService();
                    FotoModel foto = foto_service.FindByNotPredicted();
                    //Se uma foto for encontrada, tenta fazer uma predição.
                    if (foto != null)
                    {
                        var prediction = await VisionAPIService.SendImageToApi();
                        if (prediction != null)
                        {
                            DiagnosticoService diag_service = new DiagnosticoService();
                            diag_service.Insert(MontaDiagnostico(foto, prediction));

                            foto.Classificado = true;
                            foto_service.Update(foto);

                        }
                    }
                    
                }
                catch
                { 
                }
                
                await Task.Delay(System.TimeSpan.FromSeconds(60), stoppingToken);
            }
        }

        protected DiagnosticoModel MontaDiagnostico(FotoModel foto, ImagePrediction predicao)
        {
            DiagnosticoModel diag = new DiagnosticoModel();
            diag.Classificacao_Azure = predicao;
            diag.Id_plantacao = foto.Id_plantacao;
            diag.Id_Foto = foto.id;
            diag.Data= System.DateTime.Now.ToUniversalTime();
            GetDiagFinal(diag);

            return diag;

        }

        /// <summary>
        /// Analisa os dados de predicao da azure, a maior classificacao e que seja maior que 0.8 de certeza será adotada, do contrario,
        /// sem classificacao
        /// </summary>
        /// <param name="diagnostico"></param>
        protected void GetDiagFinal(DiagnosticoModel diagnostico)
        {
            double max_probability = 0.8;
            bool found = false;
            foreach (PredictionModel predicao in diagnostico.Classificacao_Azure.Predictions)
            {
                if (predicao.Probability >= max_probability)
                {
                    found = true;
                    max_probability = predicao.Probability;
                    diagnostico.Classificacao_final = predicao.TagName;
                    diagnostico.SemClassificacao = false;
                    diagnostico.Grau_certeza = predicao.Probability;
                }
            }
            if (!found)
            {
                diagnostico.Classificacao_final = "Sem classificação";
                diagnostico.SemClassificacao = true;
            }
        }


    }
}

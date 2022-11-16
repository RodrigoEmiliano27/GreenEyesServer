using System;
using System.IO;
using System.Threading.Tasks;
using Green_eyes_server.Model;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training;

namespace aspnet_core_dotnet_core.Services
{
    public class VisionAPIService
    {
        private static string endpoint = @"https://greeneyesvision-prediction.cognitiveservices.azure.com/customvision/v3.0/Prediction/21375b04-db2d-4e7d-bdd5-9923722714e2/classify/iterations/GREEN-EYES-Iteration1/image";
        private static string key = "2406ca3d0c764940bd1f84012c694fd8";
       

        private static CustomVisionPredictionClient AuthenticatePrediction(string endpoint, string predictionKey)
        {
            // Create a prediction endpoint, passing in the obtained prediction key
            CustomVisionPredictionClient predictionApi = new CustomVisionPredictionClient(new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.ApiKeyServiceClientCredentials(predictionKey))
            {
                Endpoint = endpoint
            };
            return predictionApi;
        }

        private static CustomVisionTrainingClient AuthenticateTraining(string endpoint, string trainingKey)
        {
            // Create the Api, passing in the training key
            CustomVisionTrainingClient trainingApi = new CustomVisionTrainingClient(new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.ApiKeyServiceClientCredentials(trainingKey))
            {
                Endpoint = endpoint
            };
            return trainingApi;
        }

        /// <summary>
        /// Envia uma imagem para ser classificada no serviço de visão customizada
        /// </summary>
        /// <returns></returns>
        public static async Task<ImagePrediction> SendImageToApi()
        {
            FotoService service = new FotoService();
            FotoModel foto = service.FindByNotPredicted();
            if (foto != null)
            {
                byte[] RawBase64Data = await AzureStorageHelper.GetDataFromBlob($"plant-{foto.Id_plantacao.ToString()}", foto.Nome);
                Stream stream = new MemoryStream(RawBase64Data);

                CustomVisionTrainingClient trainingApi = AuthenticateTraining("https://greeneyesvision.cognitiveservices.azure.com/",
                    "8b59529b5f4641c081741abc021f82bf");
                CustomVisionPredictionClient predictionApi = AuthenticatePrediction("https://greeneyesvision-prediction.cognitiveservices.azure.com/",
                    "2406ca3d0c764940bd1f84012c694fd8");

                Guid guid = new Guid("21375b04-db2d-4e7d-bdd5-9923722714e2");

                return predictionApi.ClassifyImage(guid, "GREEN-EYES-Iteration1", stream);
            }

            return null;           
        }

        private static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }
    }
}

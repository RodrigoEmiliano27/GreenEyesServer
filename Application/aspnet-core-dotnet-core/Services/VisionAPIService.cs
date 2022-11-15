using System;
using System.Drawing;
using System.IO;
using Green_eyes_server.Model;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;

namespace aspnet_core_dotnet_core.Services
{
    public class VisionAPIService
    {
        private static string endpoint = @"https://greeneyesvisaopragas-prediction.cognitiveservices.azure.com/customvision/v3.0/Prediction/1e5862c9-9739-4fc7-9498-3159a50f5e98/classify/iterations/Iteration1/image";
        private static string key = "a4b92b79bfa4415483b4201321c91885";
        public static Image ConvertBase64StringToImage(string image64Bit)
        {
            byte[] imageBytes = Convert.FromBase64String(image64Bit);
            return new Bitmap(new MemoryStream(imageBytes));
        }

        private static CustomVisionPredictionClient AuthenticatePrediction(string endpoint, string predictionKey)
        {
            // Create a prediction endpoint, passing in the obtained prediction key
            CustomVisionPredictionClient predictionApi = new CustomVisionPredictionClient(new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.ApiKeyServiceClientCredentials(predictionKey))
            {
                Endpoint = endpoint
            };
            return predictionApi;
        }

        public static async void SendImageToApi()
        {
            FotoService service = new FotoService();
            FotoModel foto = service.FindByNotPredicted();
            byte[] RawBase64Data = await AzureStorageHelper.GetDataFromBlob($"plant-{foto.Id_plantacao.ToString()}", foto.Nome);
            string image64Bit = Convert.ToBase64String(RawBase64Data);
            byte[] RawData= Convert.FromBase64String(image64Bit);
            Stream stream = new MemoryStream(RawData);

            FileStream fileStream = new FileStream(@"C:\Users\rodri\Downloads\WhatsApp Image 2022-11-14 at 18.38.30.jpeg",
                FileMode.Open, FileAccess.Read);


            CustomVisionPredictionClient predictionClient= AuthenticatePrediction(endpoint,key);
            /*var result = await predictionClient.ClassifyImageAsync(new Guid("b3ed1f68-67e2-4757-973b-71631107ab57"
                ), "Iteration1", fileStream, "application/octet-stream");*/

            var result = await predictionClient.ClassifyImageAsync(new Guid("b3ed1f68-67e2-4757-973b-71631107ab57"
                ), "Iteration1", stream, "application/octet-stream");
        }

        private static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }
    }
}

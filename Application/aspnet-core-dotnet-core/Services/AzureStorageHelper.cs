using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace aspnet_core_dotnet_core.Services
{
    public class AzureStorageHelper
    {
        private static string connectionstring="DefaultEndpointsProtocol=https;AccountName=storagegreeneyes;AccountKey=cq5vU4b76D0SrkTQzACk+MvS6VfV6rUhmvCRCPWYaMIncf8AGQ6jQ/VTLY1leVwk0XcVxBVB0vM1+ASt81qoKQ==;EndpointSuffix=core.windows.net";


        private static BlobServiceClient GetBlobService()
        {
            BlobServiceClient service = new BlobServiceClient(connectionstring);
            return service;
        }

        public static async Task<BlobContainerClient> CreateContainerAsync(string containerName)
        {
           
            try
            {
               BlobServiceClient blobServiceClient = GetBlobService();
               // Create the container
               BlobContainerClient container = await blobServiceClient.CreateBlobContainerAsync(containerName);

                if (await container.ExistsAsync())
                {
                    Debug.WriteLine("Created container {0}", container.Name);
                    return container;
                }
            }
            catch (RequestFailedException e)
            {
                Debug.WriteLine("HTTP error code {0}: {1}",
                                    e.Status, e.ErrorCode);
                Debug.WriteLine(e.Message);
            }

            return null;
        }

        public static async void UploadToAzure(string containerName, string fileName, string image64)
        {
            BlobContainerClient container = new BlobContainerClient(connectionstring, containerName);
            var bytes = Convert.FromBase64String(image64);
            using (var stream = new MemoryStream(bytes))
            {
                await container.UploadBlobAsync(fileName,stream);
            }
        }


        public static async Task<byte[]> GetDataFromBlob(string containerName, string fileName)
        {
            var blobClient = new BlobClient(connectionstring, containerName, fileName);
            byte[] content;
            using (var result = await blobClient.OpenReadAsync())
            {
                content = new byte[result.Length];
                result.Read(content, 0, (int)result.Length);
                //var jsonContent = System.Text.Encoding.Default.GetString(content);
                // Do whatever you want to do with this content
            }
            return content;
        }
        private static async Task<string> DownloadToText(string containerName, string fileName)
        {
            BlobClient blobClient= new BlobClient(connectionstring, containerName, fileName);
            BlobDownloadResult downloadResult = await blobClient.DownloadContentAsync();
            string downloadedData = downloadResult.Content.ToString();
            return downloadedData;
        }
    }
}

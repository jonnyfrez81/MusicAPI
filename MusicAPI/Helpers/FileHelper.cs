using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using MusicAPI.Models;
using System.IO;
using System.Threading.Tasks;

namespace MusicAPI.Helpers
{
    public static class FileHelper
    {

        public static async Task<string> UploadImage(IFormFile file)
        {
            //Storage accounts 
            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=musikkonto;AccountKey=FmSnTdFHhNJKE6n1wWkHhzxWfLIfOoJ/mzVU9m/a2qhDjIlb1/3LJZwUsaioe5yEpUUC8K6VP+3HwCON33Zg1A==;EndpointSuffix=core.windows.net";
            //ContainerName
            string containerName = "songscover";

            BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString, containerName);
            BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);

            //IO library
            var memoryStream = new MemoryStream();

            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            await blobClient.UploadAsync(memoryStream);
            return  blobClient.Uri.AbsoluteUri;

        }

        public static async Task<string> UploadFile(IFormFile file)
        {
            //Storage accounts 
            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=musikkonto;AccountKey=FmSnTdFHhNJKE6n1wWkHhzxWfLIfOoJ/mzVU9m/a2qhDjIlb1/3LJZwUsaioe5yEpUUC8K6VP+3HwCON33Zg1A==;EndpointSuffix=core.windows.net";
            //ContainerName
            string containerName = "audiofiles";

            BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString, containerName);
            BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);

            //IO library
            var memoryStream = new MemoryStream();

            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            await blobClient.UploadAsync(memoryStream);
            return blobClient.Uri.AbsoluteUri;

        }
    }
}

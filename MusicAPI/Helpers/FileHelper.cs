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
            string connectionString = //Use your AzureContiner URL!
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
            string connectionString = //Use your AzureContiner URL!
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

using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Movies.API.Enums;

namespace Movies.API.Services
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly string _azConnectionString;
        public AzureStorageService(IConfiguration configuration)
        {
            _azConnectionString = configuration.GetValue<string>("AzureStorageConnectionString");
        }
        public async Task DeleteAsync(ContainerEnum container, string blobFilename)
        {
            var containerName = Enum.GetName(typeof(ContainerEnum), container).ToLower();

            var blobContainerClient = new BlobContainerClient(_azConnectionString, containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobFilename);

            try
            {
                await blobClient.DeleteAsync();
            }
            catch
            {
            }
        }
        public async Task<string> UploadAsync(IFormFile file, ContainerEnum container, string blobName = null)
        {
            if (file.Length == 0) return null;

            var containerName = Enum.GetName(typeof(ContainerEnum), container).ToLower();

            var blobContainerClient = new BlobContainerClient(_azConnectionString, containerName);

            if (string.IsNullOrEmpty(blobName))
            {
                blobName = Guid.NewGuid().ToString();
            }
            var blobClient = blobContainerClient.GetBlobClient(blobName);
            var blobHttpHeader = new BlobHttpHeaders { ContentType = file.ContentType };

            await using (Stream stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeader });
            }

            return blobName;
        }

    }
}

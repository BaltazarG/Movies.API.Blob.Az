using Azure.Storage.Blobs.Models;
using Movies.API.Enums;

namespace Movies.API.Services
{
    public interface IAzureStorageService
    {
        Task<string> UploadAsync(IFormFile file, ContainerEnum container, string blobName = null);
        Task DeleteAsync(ContainerEnum container, string blobFilename);
    }
}

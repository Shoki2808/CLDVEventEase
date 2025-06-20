using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EventEaseAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace EventEaseAPI.Services
{
    public class BlobService : IBlobService
    {
        private readonly IConfiguration _configuration;

        public BlobService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> UploadAsync(IFormFile file, string containerName)
        {
            var connectionString = _configuration["AzureBlobStorage:ConnectionString"];
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Azure Blob Storage connection string is not configured.");

            var blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

            var blobName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var blobClient = containerClient.GetBlobClient(blobName);

            try
            {
                await using var stream = file.OpenReadStream();
                await blobClient.UploadAsync(stream, overwrite: true);
            }
            catch (RequestFailedException ex)
            {
                throw new InvalidOperationException("Failed to upload blob to Azure Storage.", ex);
            }

            return blobClient.Uri.ToString();
        }

        public async Task<bool> DeleteAsync(string blobUrl, string containerName)
        {
            var connectionString = _configuration["AzureBlobStorage:ConnectionString"];
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Azure Blob Storage connection string is not configured.");

            var blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            var blobName = Path.GetFileName(new Uri(blobUrl).LocalPath);
            var blobClient = containerClient.GetBlobClient(blobName);

            try
            {
                return await blobClient.DeleteIfExistsAsync();
            }
            catch (RequestFailedException ex)
            {
                throw new InvalidOperationException("Failed to delete blob from Azure Storage.", ex);
            }
        }
    }
}

using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;

namespace EventEaseBooking.Services
{
    public interface IAzureBlobStorageService
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName);
    }

    public class AzureBlobStorageService : IAzureBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;


        public AzureBlobStorageService(IConfiguration configuration)
        {
            //var accountName = "yourstorageaccount";
            //var storageUri = new Uri($"https://{accountName}.blob.core.windows.net");
            //_blobServiceClient = new BlobServiceClient(storageUri, new DefaultAzureCredential());

            var connectionString = configuration.GetConnectionString("AzureBlobStorage");
            _containerName = configuration["AzureBlobContainerName"];
            _blobServiceClient = new BlobServiceClient(connectionString.Trim());

            // Test the connection
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            containerClient.CreateIfNotExists(); // This will throw if connection is invalid


            // Validate connection string
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException("Azure Storage connection string is not configured");
            }
        }
        //, string contentType


        public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
        {
            try
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
                await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

                var blobClient = containerClient.GetBlobClient(fileName);
                await blobClient.UploadAsync(fileStream, overwrite: true);
                //await blobClient.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contentType });
                return blobClient.Uri.ToString();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }

        }

        public async Task DeleteFileAsync(string blobUrl)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobName = new Uri(blobUrl).Segments.Last();
            await containerClient.DeleteBlobIfExistsAsync(blobName);
        }


    }
}

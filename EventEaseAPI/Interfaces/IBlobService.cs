namespace EventEaseAPI.Interfaces
{
    public interface IBlobService
    {
            Task<string> UploadAsync(IFormFile file, string containerName);
            Task<bool> DeleteAsync(string blobUrl, string containerName);

    }
}

using EventEaseAPI.SearchModels;

namespace EventEaseAPI.Interfaces
{
    public interface IAzureSearchService<T>
    {
        Task UploadDocumentsAsync(IEnumerable<T> documents);
        Task<List<T>> SearchAsync(string searchText);
    }
}

using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace EventEaseAPI.SearchModels;

public class ClientSearchModel
{
    [SimpleField(IsKey = true)]
    public string ClientId { get; set; }

    [SearchableField]
    public string ClientName { get; set; }

    [SearchableField]
    public string Email { get; set; }

    [SearchableField]
    public string Phone { get; set; }
}

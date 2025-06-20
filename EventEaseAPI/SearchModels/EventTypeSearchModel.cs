using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace EventEaseAPI.SearchModels;

public class EventTypeSearchModel
{
    [SimpleField(IsKey = true)]
    public string EventTypeId { get; set; }

    [SearchableField]
    public string EventTypeName { get; set; }

    [SearchableField]
    public string Description { get; set; }
}

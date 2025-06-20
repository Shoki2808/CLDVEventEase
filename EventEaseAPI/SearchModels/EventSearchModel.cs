using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace EventEaseAPI.SearchModels;

public class EventSearchModel
{
    [SimpleField(IsKey = true)]
    public string EventId { get; set; }

    [SearchableField]
    public string EventName { get; set; }

    [SearchableField]
    public string Description { get; set; }

    [SearchableField]
    public string VenueName { get; set; }

    [SearchableField]
    public string EventTypeName { get; set; }

    public DateOnly EventDate { get; set; }
}

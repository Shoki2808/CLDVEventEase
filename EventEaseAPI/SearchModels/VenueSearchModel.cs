using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace EventEaseAPI.SearchModels;

public class VenueSearchModel
{
    [SimpleField(IsKey = true)]
    public string VenueId { get; set; } = Guid.NewGuid().ToString();

    [SearchableField(AnalyzerName = "en.lucene")]
    public string VenueName { get; set; }

    [SearchableField]
    public string Location { get; set; }

    [SimpleField(IsFilterable = true)]
    public int Capacity { get; set; }

    [SimpleField(IsFilterable = true)]
    public bool IsActive { get; set; }
}

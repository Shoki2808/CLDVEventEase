using Azure.Search.Documents.Indexes;

namespace EventEaseAPI.SearchModels
{
    public class BookingSearchModel
    {
          
        [SimpleField(IsKey = true)]
        public string BookingId { get; set; }

        [SimpleField(IsFilterable = true, IsSortable = true)]
        public string BookingDate { get; set; }

        [SimpleField(IsFilterable = true, IsSortable = true)]
        public string BookingTime { get; set; }

        [SearchableField]
        public string EventName { get; set; }

        [SearchableField]
        public string EventTypeName { get; set; }

        [SearchableField]
        public string VenueName { get; set; }

        [SearchableField]
        public string ClientName { get; set; }
    }
}


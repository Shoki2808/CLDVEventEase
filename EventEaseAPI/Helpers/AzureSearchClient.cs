using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Models;
using EventEaseAPI.SearchModels;
using Microsoft.Extensions.Configuration;

namespace EventEaseAPI.Helpers;

public class AzureSearchClient
{
    private readonly IConfiguration _config;
    private readonly string _serviceEndpoint;
    private readonly string _adminApiKey;

    public AzureSearchClient(IConfiguration config)
    {
        _config = config;
        _serviceEndpoint = config["AzureSearch:Endpoint"]!;
        _adminApiKey = config["AzureSearch:ApiKey"]!;
    }

    private Uri Endpoint => new(_serviceEndpoint);
    private AzureKeyCredential Credential => new(_adminApiKey);

    private SearchIndexClient GetIndexClient() => new(Endpoint, Credential);
    private SearchClient GetSearchClient(string indexName) => new(Endpoint, indexName, Credential);

    // ---------- VENUE ----------
    public async Task CreateVenueIndexAsync()
    {
        var indexClient = GetIndexClient();
        var fields = new FieldBuilder().Build(typeof(VenueSearchModel));
        var index = new SearchIndex("venues", fields);
        await indexClient.CreateOrUpdateIndexAsync(index);
    }

    public async Task UploadVenuesAsync(IEnumerable<VenueSearchModel> venues)
    {
        var searchClient = GetSearchClient("venues");
        await searchClient.UploadDocumentsAsync(venues);
    }

    public async Task<List<VenueSearchModel>> SearchVenuesAsync(string query)
    {
        var client = GetSearchClient("venues");
        var results = await client.SearchAsync<VenueSearchModel>(query);
        return results.Value.GetResults().Select(r => r.Document).ToList();
    }

    // ---------- BOOKING ----------
    public async Task CreateBookingIndexAsync()
    {
        var indexClient = GetIndexClient();
        var fields = new FieldBuilder().Build(typeof(BookingSearchModel));
        var index = new SearchIndex("bookings", fields);
        await indexClient.CreateOrUpdateIndexAsync(index);
    }

    public async Task UploadBookingsAsync(IEnumerable<BookingSearchModel> bookings)
    {
        var client = GetSearchClient("bookings");
        await client.UploadDocumentsAsync(bookings);
    }

    public async Task<List<BookingSearchModel>> SearchBookingsAsync(string query)
    {
        var client = GetSearchClient("bookings");
        var results = await client.SearchAsync<BookingSearchModel>(query);
        return results.Value.GetResults().Select(r => r.Document).ToList();
    }

    // ---------- EVENT ----------
    public async Task CreateEventIndexAsync()
    {
        var indexClient = GetIndexClient();
        var fields = new FieldBuilder().Build(typeof(EventSearchModel));
        var index = new SearchIndex("events", fields);
        await indexClient.CreateOrUpdateIndexAsync(index);
    }

    public async Task UploadEventsAsync(IEnumerable<EventSearchModel> events)
    {
        var client = GetSearchClient("events");
        await client.UploadDocumentsAsync(events);
    }

    public async Task<List<EventSearchModel>> SearchEventsAsync(string query)
    {
        var client = GetSearchClient("events");
        var results = await client.SearchAsync<EventSearchModel>(query);
        return results.Value.GetResults().Select(r => r.Document).ToList();
    }

    // ---------- CLIENT ----------
    public async Task CreateClientIndexAsync()
    {
        var indexClient = GetIndexClient();
        var fields = new FieldBuilder().Build(typeof(ClientSearchModel));
        var index = new SearchIndex("clients", fields);
        await indexClient.CreateOrUpdateIndexAsync(index);
    }

    public async Task UploadClientsAsync(IEnumerable<ClientSearchModel> clients)
    {
        var client = GetSearchClient("clients");
        await client.UploadDocumentsAsync(clients);
    }

    public async Task<List<ClientSearchModel>> SearchClientsAsync(string query)
    {
        var client = GetSearchClient("clients");
        var results = await client.SearchAsync<ClientSearchModel>(query);
        return results.Value.GetResults().Select(r => r.Document).ToList();
    }

    // ---------- EVENT TYPE ----------
    public async Task CreateEventTypeIndexAsync()
    {
        var indexClient = GetIndexClient();
        var fields = new FieldBuilder().Build(typeof(EventTypeSearchModel));
        var index = new SearchIndex("eventtypes", fields);
        await indexClient.CreateOrUpdateIndexAsync(index);
    }

    public async Task UploadEventTypesAsync(IEnumerable<EventTypeSearchModel> eventTypes)
    {
        var client = GetSearchClient("eventtypes");
        await client.UploadDocumentsAsync(eventTypes);
    }

    public async Task<List<EventTypeSearchModel>> SearchEventTypesAsync(string query)
    {
        var client = GetSearchClient("eventtypes");
        var results = await client.SearchAsync<EventTypeSearchModel>(query);
        return results.Value.GetResults().Select(r => r.Document).ToList();
    }
}

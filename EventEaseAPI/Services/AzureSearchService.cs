using EventEaseAPI.Helpers;
using EventEaseAPI.SearchModels;

namespace EventEaseAPI.Services
{
    public class AzureSearchService
    {
        private readonly AzureSearchClient _searchClient;

        public AzureSearchService(AzureSearchClient searchClient)
        {
            _searchClient = searchClient;
        }

        // ---------------- VENUE ----------------
        public async Task ReindexVenuesAsync(IEnumerable<Venue> venues)
        {
            var searchModels = venues.Select(v => new VenueSearchModel
            {
                VenueId = v.VenueId.ToString(),
                VenueName = v.VenueName,
                Location = v.Location,
                Capacity = v.Capacity,
                IsActive = v.IsActive
            });

            await _searchClient.CreateVenueIndexAsync();
            await _searchClient.UploadVenuesAsync(searchModels);
        }

        public async Task<List<VenueSearchModel>> SearchVenuesAsync(string query)
        {
            return await _searchClient.SearchVenuesAsync(query);
        }

        // ---------------- BOOKING ----------------
        public async Task ReindexBookingsAsync(IEnumerable<Booking> bookings)
        {
            var searchModels = bookings.Select(b => new BookingSearchModel
            {
                BookingId = b.BookingId.ToString(),
                BookingDate = b.BookingDate.ToString("yyyy-MM-dd"),
                BookingTime = b.BookingTime.ToString("HH:mm"),
                EventName = b.Event?.EventName ?? "",
                VenueName = b.Event?.Venue?.VenueName ?? "",
                ClientName = b.Client?.ClientName ?? "",
                EventTypeName = b.Event?.EventType.EventTypeName ?? ""
            });

            await _searchClient.CreateBookingIndexAsync();
            await _searchClient.UploadBookingsAsync(searchModels);
        }

        public async Task<List<BookingSearchModel>> SearchBookingsAsync(string query)
        {
            return await _searchClient.SearchBookingsAsync(query);
        }

        // ---------------- EVENT ----------------
        public async Task ReindexEventsAsync(IEnumerable<Event> events)
        {
            var searchModels = events.Select(e => new EventSearchModel
            {
                EventId = e.EventId.ToString(),
                EventName = e.EventName,
                EventTypeName = e.EventType?.EventTypeName ?? "",
                VenueName = e.Venue?.VenueName ?? "",
                Description = e.EventDescription ?? ""
            });

            await _searchClient.CreateEventIndexAsync();
            await _searchClient.UploadEventsAsync(searchModels);
        }

        public async Task<List<EventSearchModel>> SearchEventsAsync(string query)
        {
            return await _searchClient.SearchEventsAsync(query);
        }

        // ---------------- CLIENT ----------------
        public async Task ReindexClientsAsync(IEnumerable<Client> clients)
        {
            var searchModels = clients.Select(c => new ClientSearchModel
            {
                ClientId = c.ClientId.ToString(),
                ClientName = c.ClientName,
                Email = c.ContactDetails,
                Phone = c.ContactDetails
            });

            await _searchClient.CreateClientIndexAsync();
            await _searchClient.UploadClientsAsync(searchModels);
        }

        public async Task<List<ClientSearchModel>> SearchClientsAsync(string query)
        {
            return await _searchClient.SearchClientsAsync(query);
        }

        // ---------------- EVENT TYPE ----------------
        public async Task ReindexEventTypesAsync(IEnumerable<EventType> eventTypes)
        {
            var searchModels = eventTypes.Select(et => new EventTypeSearchModel
            {
                EventTypeId = et.EventTypeId.ToString(),
                EventTypeName = et.EventTypeName
            });

            await _searchClient.CreateEventTypeIndexAsync();
            await _searchClient.UploadEventTypesAsync(searchModels);
        }

        public async Task<List<EventTypeSearchModel>> SearchEventTypesAsync(string query)
        {
            return await _searchClient.SearchEventTypesAsync(query);
        }
    }
}

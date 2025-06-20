using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventEaseAPI.Services;
using EventEaseAPI.SearchModels;
using EventEaseAPI.Data.ApplicationDbContext;

namespace EventEaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly AzureSearchService _searchService;
        private readonly AppDbContext _context;

        public SearchController(AzureSearchService searchService, AppDbContext context)
        {
            _searchService = searchService;
            _context = context;
        }

        // ---------- VENUES ----------
        [HttpGet("venues")]
        public async Task<ActionResult<IEnumerable<VenueSearchModel>>> SearchVenues(string query)
        {
            var results = await _searchService.SearchVenuesAsync(query);
            return Ok(results);
        }

        [HttpPost("venues/reindex")]
        public async Task<IActionResult> ReindexVenues()
        {
            var venues = _context.Venues.ToList();
            await _searchService.ReindexVenuesAsync(venues);
            return Ok("Venues reindexed.");
        }

        // ---------- BOOKINGS ----------
        [HttpGet("bookings")]
        public async Task<ActionResult<IEnumerable<BookingSearchModel>>> SearchBookings(string query)
        {
            var results = await _searchService.SearchBookingsAsync(query);
            return Ok(results);
        }

        [HttpPost("bookings/reindex")]
        public async Task<IActionResult> ReindexBookings()
        {
            var bookings = _context.Bookings
                .Include(b => b.Client)
                .Include(b => b.Event)
                    .ThenInclude(e => e.Venue)
                .ToList();

            await _searchService.ReindexBookingsAsync(bookings);
            return Ok("Bookings reindexed.");
        }

        // ---------- EVENTS ----------
        [HttpGet("events")]
        public async Task<ActionResult<IEnumerable<EventSearchModel>>> SearchEvents(string query)
        {
            var results = await _searchService.SearchEventsAsync(query);
            return Ok(results);
        }

        [HttpPost("events/reindex")]
        public async Task<IActionResult> ReindexEvents()
        {
            var events = _context.Events
                .Include(e => e.Venue)
                .Include(e => e.EventType)
                .ToList();

            await _searchService.ReindexEventsAsync(events);
            return Ok("Events reindexed.");
        }

        // ---------- CLIENTS ----------
        [HttpGet("clients")]
        public async Task<ActionResult<IEnumerable<ClientSearchModel>>> SearchClients(string query)
        {
            var results = await _searchService.SearchClientsAsync(query);
            return Ok(results);
        }

        [HttpPost("clients/reindex")]
        public async Task<IActionResult> ReindexClients()
        {
            var clients = _context.Clients.ToList();
            await _searchService.ReindexClientsAsync(clients);
            return Ok("Clients reindexed.");
        }

        // ---------- EVENT TYPES ----------
        [HttpGet("eventtypes")]
        public async Task<ActionResult<IEnumerable<EventTypeSearchModel>>> SearchEventTypes(string query)
        {
            var results = await _searchService.SearchEventTypesAsync(query);
            return Ok(results);
        }

        [HttpPost("eventtypes/reindex")]
        public async Task<IActionResult> ReindexEventTypes()
        {
            var eventTypes = _context.EventTypes.ToList();
            await _searchService.ReindexEventTypesAsync(eventTypes);
            return Ok("Event types reindexed.");
        }
    }
}

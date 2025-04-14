using EventEaseBooking.Components.Pages.Event;
using EventEaseBooking.Interfaces;
using EventEaseBooking.Models;
using EventEaseBooking.Static;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;


namespace EventEaseBooking.Services
{


    public class Eventservice : IEventService
    {

        private readonly HttpClient httpClient;


    public Eventservice(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task<List<EventType>> GetEventTypes()
        {
            var result = new List<EventType>();
            try
            {
                var url = Endpoints.BaseUrl + "api/EventType/GetAllEventTypes";
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                // request.Content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                //var response = await client.GetAsync(url);
                client.BaseAddress = new Uri(Endpoints.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<EventType>>(responseData);
                }

            }
            catch (Exception e)
            {
                throw new Exception($"Request Unsuccessful. {e.Message}");

            }
            return result;
        }

        public async Task<List<Event>> GetEvents()
        {
            var result = new List<Event>();
            try
            {
                var url = Endpoints.BaseUrl + "api/Event/GetAllEvents";
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                // request.Content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                //var response = await client.GetAsync(url);
                client.BaseAddress = new Uri(Endpoints.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<Event>>(responseData);
                }

            }
            catch (Exception e)
            {
                throw new Exception($"Request Unsuccessful. {e.Message}");

            }
            return result;
        }

        public async Task<Event> AddEvent(Event eventModel)
        {
            Event? result = new Event();
            try
            {
                var url = Endpoints.BaseUrl + "api/Event/CreateEvent";
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = new StringContent(JsonConvert.SerializeObject(eventModel), Encoding.UTF8, "application/json");

                //HttpClient client = httpClient.CreateClient();
                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(Endpoints.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<Event?>(responseData);
                }
            }
            catch
            {
                throw new Exception("Request Unsuccessful.");
            }
            return result ?? new Event();
        }


        public async Task<Event> GetEventById(int eventId)
        {
            Event? result = null;
            try
            {
                var url = Endpoints.BaseUrl + $"api/Event/GetEventById/{eventId}";
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Endpoints.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<Event>(responseData);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Event not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to fetch booking: {ex.Message}");
            }
            return result ?? throw new Exception("Booking data is null.");
        }


        //List<EventType> EventService.GetEventTypes()
        //{
        //    throw new NotImplementedException();
        //}
    }
}

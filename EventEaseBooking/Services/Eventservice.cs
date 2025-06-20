using EventEaseBooking.Components.Pages.Event;
using EventEaseBooking.Interfaces;
using EventEaseBooking.Models;
using EventEaseBooking.Static;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Radzen;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
                //else
                //{
                //    // Handle different error cases
                //    var errorResponse = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                //    var errorMessage = errorResponse?.Title ?? "An error occurred";

                //    switch (response.StatusCode)
                //    {
                //        case HttpStatusCode.BadRequest:
                //            if (errorResponse.Extensions.TryGetValue("message", out var messageObj))
                //            {
                //                errorMessage = messageObj.ToString();
                //            }
                //            throw new ApplicationException(errorMessage);

                //            break;

                //        default:
                    
                //            break;
                //    }
                //}
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Failed to create event", ex);
            }
            return result ?? new Event();
        }


        public async Task<Event> GetEventById(int eventId)
        {
            Event? result = null;
            try
            {
                var url = Endpoints.BaseUrl + $"api/Event/GetEventById/";

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Endpoints.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                HttpResponseMessage response = await client.GetAsync(url + "?id=" + eventId);

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


        public async Task<Event> UpdateEvent(Event eventModel)
        {
            Event? result = new();
            try
            {
                var url = $"api/Event/UpdateEvent/?id={eventModel.EventId}";
                var request = new HttpRequestMessage(HttpMethod.Put, url);
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(eventModel),
                    Encoding.UTF8,
                    "application/json"
                );

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Endpoints.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<Event>(responseData);
                }
            }
            catch
            {
                throw new Exception("Update request failed.");
            }
            return result;
        }
        //public async Task<Event> UpdateEvent(Event eventModel)
        //{
        //    Event result = new Event();
        //    if (eventModel == null)
        //        throw new ArgumentNullException(nameof(eventModel));

        //    if (eventModel.EventId <= 0)
        //        throw new ArgumentException("Event ID must be valid.");

        //    try
        //    {
        //        using (var client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri(Endpoints.BaseUrl);
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //            // Add auth token if needed
        //            // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //            var url = $"api/Event/UpdateEvent/?id={eventModel.EventId}";
        //            var jsonContent = JsonConvert.SerializeObject(eventModel);

        //            //// Log request (remove in production)
        //            //Console.WriteLine($"Request JSON: {jsonContent}");

        //            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        //            using (var response = await client.PutAsync(url, content))
        //            {
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    var responseData = await response.Content.ReadAsStringAsync();
        //                     result =  JsonConvert.DeserializeObject<Event>(responseData);
                           
        //                }
        //            }
        //         return result ?? new Event();
        //        }
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        throw new Exception($"Network error: {ex.Message}", ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Failed to update event: {ex.Message}", ex);
        //    }
        //}
        //List<EventType> EventService.GetEventTypes()
        //{
        //    throw new NotImplementedException();
        //}
    }
}

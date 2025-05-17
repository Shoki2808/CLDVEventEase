using EventEaseBooking.Interfaces;
using EventEaseBooking.Models;
using EventEaseBooking.Static;
using Newtonsoft.Json;
using System.Text;

namespace EventEaseBooking.Services
{
    public class VenueService : IVenueService
    {
        private readonly HttpClient httpClient;

        public VenueService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Venue> AddVenue(Venue venueModel)
        {
            Venue? result = new Venue();
            try
            {
                var url = Endpoints.BaseUrl + "api/Venue/CreateVenue";
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = new StringContent(JsonConvert.SerializeObject(venueModel), Encoding.UTF8, "application/json");

                //HttpClient client = httpClient.CreateClient();
                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(Endpoints.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<Venue?>(responseData);
                }
            }
            catch
            {
                throw new Exception("Request Unsuccessful.");
            }
            return result ?? new Venue();
        }



        public async Task<List<Venue>> GetVenues()
        {
            var result = new List<Venue>();
            try
            {
                var url = Endpoints.BaseUrl + "api/Venue/GetAllClients";
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                // request.Content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                //var response = await client.GetAsync(url);
                client.BaseAddress = new Uri(Endpoints.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<Venue>>(responseData);
                }

            }
            catch (Exception e)
            {
                throw new Exception($"Request Unsuccessful. {e.Message}");

            }
            return result;
        }

        public async Task<Venue> GetVenueById(int id)
        {
            Venue? result = null;
            try
            {
                var url = Endpoints.BaseUrl + $"api/Venue/GetVenueById";
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Endpoints.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                HttpResponseMessage response = await client.GetAsync(url + "?id=" + id);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<Venue>(responseData);
                }
                //else if (response.StatusCode == HttpStatusCode.NotFound)
                //{

                //}
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to fetch booking: {ex.Message}");
            }
            return result ?? throw new Exception("Booking data is null.");
        }


        public async Task<Venue> UpdateVenue(Venue venueModel)
        {
            Venue? result = new();
            try
            {
                var url = "api/Venue/UpdateVenue";
                var request = new HttpRequestMessage(HttpMethod.Put, url);
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(venueModel),
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
                    result = JsonConvert.DeserializeObject<Venue>(responseData);
                }
            }
            catch
            {
                throw new Exception("Update request failed.");
            }
            return result;
        }
    }
}

using EventEaseBooking.Interfaces;
using EventEaseBooking.Models;
using EventEaseBooking.Static;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;

namespace EventEaseBooking.Services
{
    public class Bookingservice : IBookingService
    {
        private readonly HttpClient httpClient = new HttpClient();

        private List<Venue> Venues = new List<Venue>
        {
            new Venue { VenueId = 1, VenueName = "Main Hall", Location = "Downtown", Capacity = 200 },
            new Venue { VenueId = 2, VenueName = "Small Hall", Location = "Uptown", Capacity = 50 },
        };


        private List<Booking> Bookings = new List<Booking>();

        public Bookingservice(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<Venue>> GetVenues()
        {
            var result = new List<Venue>();
            try
            {
                    var url = Endpoints.BaseUrl + "api/Admin/GetAllVenues";
                    //var request = new HttpRequestMessage(HttpMethod.Get, url);
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

        public async Task<Booking> CreateBooking(Booking bookingModel)
        {
            Booking? result = new();
            try
            {
                var url = Endpoints.BaseUrl + "api/Booking/CreateBooking";
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = new StringContent(JsonConvert.SerializeObject(bookingModel), Encoding.UTF8, "application/json");

                //HttpClient client = httpClient.CreateClient();
                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(Endpoints.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<Booking>(responseData);
                }
            }
            catch
            {
                throw new Exception("Request Unsuccessful.");
            }
            return result;
        }


        public async Task<Booking> UpdateBooking(Booking bookingModel)
        {
            Booking? result = new();
            try
            {
                var url = "api/Booking/UpdateBooking";
                var request = new HttpRequestMessage(HttpMethod.Put, url);
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(bookingModel),
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
                    result = JsonConvert.DeserializeObject<Booking>(responseData);
                }
            }
            catch
            {
                throw new Exception("Update request failed.");
            }
            return result;
        }


        public async Task<List<Booking>> GetBookings()
        {
            var result = new List<Booking>();
            try
            {
                //result =  await httpClient.GetFromJsonAsync<List<Booking>>("api/Booking/GetAllBookings") ?? new List<Booking>();
                var url = "https://eventeaseapi-a7h4b6f9a6fxhjfb.southafricanorth-01.azurewebsites.net/api/Booking/GetAllBookings";
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                // request.Content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                //var response = await client.GetAsync(url);
                //client.BaseAddress = new Uri(Endpoints.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<Booking>>(responseData);
                }

            }
            catch (Exception e)
            {
                throw new Exception($"Request Unsuccessful. {e.Message}");

            }
            return result;
        }

        public async Task<Booking> GetBookingById(int bookingId)
        {
            Booking? result = null;
            try
            {
                var url = Endpoints.BaseUrl + $"api/Booking/GetBookingById";
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Endpoints.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                HttpResponseMessage response = await client.GetAsync(url + "?id=" + bookingId);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<Booking>(responseData);
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


        public async Task<bool> DeleteBooking(int bookingId)
        {
            try
            {
                var url = Endpoints.BaseUrl + $"api/Booking/DeleteBooking?{bookingId}";
                var request = new HttpRequestMessage(HttpMethod.Put, url);

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Endpoints.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                HttpResponseMessage response = await client.SendAsync(request);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                throw new Exception("Delete request failed.");
            }
        }

        //public void BookEvent(int eventId, int clientId)
        //{
        //    var selectedEvent = Events.FirstOrDefault(e => e.EventId == eventId);
        //    if (selectedEvent != null)
        //    {
        //        var booking = new BookingModel
        //        {
        //            ClientId = clientId,
        //            EventId = eventId,
        //            //Event = selectedEvent,
        //            BookingDate = DateTime.Now
        //        };
        //        Bookings.Add(booking);
        //    }
        //}



    }
}

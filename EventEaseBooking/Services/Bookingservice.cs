using EventEaseBooking.Interfaces;
using EventEaseBooking.Models;
using EventEaseBooking.Static;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace EventEaseBooking.Services
{
    public class Bookingservice : IBookingService
    {
        private readonly HttpClient httpClient = new HttpClient();


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
                    var url = Endpoints.BaseUrl + "api/Venue/GetAllVenues";
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

        //public async Task<Booking> CreateBooking(Booking bookingModel)
        //{
        //    Booking? result = new();
        //    try
        //    {
        //        var url = Endpoints.BaseUrl + "api/Booking/CreateBooking";
        //        var request = new HttpRequestMessage(HttpMethod.Post, url);
        //        request.Content = new StringContent(JsonConvert.SerializeObject(bookingModel), Encoding.UTF8, "application/json");

        //        //HttpClient client = httpClient.CreateClient();
        //        HttpClient client = new HttpClient();

        //        client.BaseAddress = new Uri(Endpoints.BaseUrl);
        //        client.DefaultRequestHeaders.Add("Accept", "application/json");
        //        HttpResponseMessage response = await client.SendAsync(request);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var responseData = await response.Content.ReadAsStringAsync();
        //            result = JsonConvert.DeserializeObject<Booking>(responseData);
        //        }
        //    }
        //    catch
        //    {
        //        throw new Exception("Request Unsuccessful.");
        //    }
        //    return result;
        //}
        
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
                var url = $"api/Booking/UpdateBooking/{bookingModel.BookingId}";
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

        //public async Task<Booking> UpdateBooking(Booking bookingModel)
        //{
        //    if (bookingModel == null)
        //        throw new ArgumentNullException(nameof(bookingModel));

        //    if (bookingModel.BookingId <= 0)
        //        throw new ArgumentException("Booking ID must be valid.");

        //    try
        //    {
        //        using (var client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri(Endpoints.BaseUrl);
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //            // Add auth token if needed
        //            // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //            var url = $"api/Booking/UpdateBooking/?id={bookingModel.BookingId}";
        //            var jsonContent = JsonConvert.SerializeObject(bookingModel);

        //            //// Log request (remove in production)
        //            //Console.WriteLine($"Request JSON: {jsonContent}");

        //            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        //            using (var response = await client.PutAsync(url, content))
        //            {
        //                if (!response.IsSuccessStatusCode)
        //                {
        //                    // Read error response
        //                    var errorContent = await response.Content.ReadAsStringAsync();
        //                    throw new Exception($"API Error: {response.StatusCode} - {errorContent}");
        //                }

        //                var responseData = await response.Content.ReadAsStringAsync();
        //                return JsonConvert.DeserializeObject<Booking>(responseData)
        //                    ?? throw new InvalidOperationException("API returned null booking.");
        //            }
        //        }
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        throw new Exception($"Network error: {ex.Message}", ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Failed to update booking: {ex.Message}", ex);
        //    }
        //}

        public async Task<List<Booking>> GetBookings()
        {
            var result = new List<Booking>();
            try
            {
                //result =  await httpClient.GetFromJsonAsync<List<Booking>>("api/Booking/GetAllBookings") ?? new List<Booking>();
                //var url = "https://eventeaseapi-a7h4b6f9a6fxhjfb.southafricanorth-01.azurewebsites.net/api/Booking/GetAllBookings"; 
                var url = Endpoints.BaseUrl + "api/Booking/GetAllBookings";
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

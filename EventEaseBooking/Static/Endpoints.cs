namespace EventEaseBooking.Static
{
    public class Endpoints
    {
        public static string BaseUrl { get; set; }

        static Endpoints()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

            var root = builder.Build();
            BaseUrl = root.GetValue<string>("Endpoints:BaseUrl");
        }
    }
}

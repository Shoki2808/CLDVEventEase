namespace EventEaseAPI.DTOs.Client
{
    public class ClientReadDto
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ContactDetails { get; set; }
        public string ClientEmail { get; set; }
        public bool IsActive { get; set; }
    }
}

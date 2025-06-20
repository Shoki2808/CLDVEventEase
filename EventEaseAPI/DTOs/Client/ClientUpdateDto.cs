namespace EventEaseAPI.DTOs.Client
{
    public class ClientUpdateDto
    {
        public string ClientName { get; set; }
        public string ContactDetails { get; set; }
        public string ClientEmail { get; set; }
        public bool IsActive { get; set; }
    }
}

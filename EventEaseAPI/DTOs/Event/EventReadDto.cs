namespace EventEaseAPI.DTOs.Event
{
    public class EventReadDto
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateOnly EventDate { get; set; }

        public TimeOnly StartTime { get; set; } = new TimeOnly();

        public TimeOnly EndTime { get; set; } = new TimeOnly(); 
        public string EventDescription { get; set; }
        public string VenueName { get; set; }
        public string EventTypeName { get; set; }
       // public bool IsActive { get; set; }

        public string Status { get; set; } = "Inactive";
    }
}

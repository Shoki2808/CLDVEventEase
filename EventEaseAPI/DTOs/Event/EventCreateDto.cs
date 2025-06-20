namespace EventEaseAPI.DTOs.Event
{
    public class EventCreateDto
    {
        public string EventName { get; set; } = string.Empty;

        public DateOnly EventDate { get; set; }

        public TimeOnly StartTime { get; set; } = new TimeOnly();

        public TimeOnly EndTime { get; set; } = new TimeOnly();

        public string EventDescription { get; set; } = string.Empty;

        public int VenueId { get; set; }

        public int EventTypeId { get; set; }
    }
}

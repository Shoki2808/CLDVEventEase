namespace EventEaseAPI.DTOs.Event
{
    public class EventUpdateDto
    {
        public string EventName { get; set; }
        public DateOnly EventDate { get; set; }

        public TimeOnly StartTime { get; set; } = new TimeOnly();

        public TimeOnly EndTime { get; set; } = new TimeOnly();
        public string EventDescription { get; set; }
        public int VenueId { get; set; }
        public int EventTypeId { get; set; }
        public bool IsActive { get; set; }
    }
}

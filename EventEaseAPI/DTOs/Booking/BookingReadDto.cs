namespace EventEaseAPI.DTOs.Booking
{
    public class BookingReadDto
    {
        public int BookingId { get; set; }

        public DateOnly BookingDate { get; set; }

        public TimeOnly BookingTime { get; set; }

        //public int EventId { get; set; }

        public string? EventName { get; set; }

        public TimeOnly StartTime { get; set; } 

        public TimeOnly EndTime { get; set; }

        public string EventTypeName { get; set; }

        //public int? ClientId { get; set; }

        public string? ClientName { get; set; }

        //public bool IsActive { get; set; }
        public string? VenueName { get; internal set; }

        public string AvailabilityStatus { get; set; } = "Inactive";
    }
}

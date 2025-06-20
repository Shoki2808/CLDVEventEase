﻿namespace EventEaseAPI.DTOs.Venue
{
    public class VenueReadDto
    {
        public int VenueId { get; set; }
        public string VenueName { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public string ImageUrl { get; set; }

        public string AvailabilityStatus { get; set; } = "Booked";
    }
}

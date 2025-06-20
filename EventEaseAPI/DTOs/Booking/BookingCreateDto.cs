using System.ComponentModel.DataAnnotations;

namespace EventEaseAPI.DTOs.Booking
{
    public class BookingCreateDto
    {
        [Required(ErrorMessage = "Booking date is required.")]
        public DateOnly BookingDate { get; set; }

        [Required(ErrorMessage = "Booking time is required.")]
        public TimeOnly BookingTime { get; set; }

        [Required(ErrorMessage = "Event ID is required.")]
        public int EventId { get; set; }

        public int? ClientId { get; set; }
    }
}

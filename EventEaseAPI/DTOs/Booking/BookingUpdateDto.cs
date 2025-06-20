using System.ComponentModel.DataAnnotations;

namespace EventEaseAPI.DTOs.Booking
{
    public class BookingUpdateDto
    {
        [Required]
        public DateOnly BookingDate { get; set; }

        [Required]
        public TimeOnly BookingTime { get; set; }

        [Required]
        public int EventId { get; set; }

        public int? ClientId { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}

using EventEaseAPI.DTOs.Booking;

namespace EventEaseAPI.Interfaces
{
    public interface IBookingService
    {

        Task<IEnumerable<BookingReadDto>> GetAllBookingsAsync();
        Task<BookingReadDto?> GetBookingByIdAsync(int id);
        Task<BookingReadDto> CreateBookingAsync(BookingCreateDto dto);
        Task<bool> UpdateBookingAsync(int id, BookingUpdateDto dto);
        Task<bool> DeleteBookingAsync(int id);

    }
}

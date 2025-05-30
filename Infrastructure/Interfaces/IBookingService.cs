using Domain.ApiResponse;
using Domain.DTOs.BookingDTOs;

namespace Infrastructure.Interfaces;

public interface IBookingService
{
    public Task<Response<List<GetBookingDTO>>> GetBookingsAsync();
    public Task<Response<GetBookingDTO>> GetBookingByIdAsync(int id);
    public Task<Response<string>> CreateBookingAsync(CreateBookingDTO dto);
    public Task<Response<string>> UpdateBookingAsync(UpdateBookingDTO dto);
    public Task<Response<string>> DeleteBookingAsync(int id);
    public Task<Response<List<ActiveBookingDTO>>> GetActiveBookingsAsync();


}

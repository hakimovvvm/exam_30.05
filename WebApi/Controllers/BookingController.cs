using Domain.ApiResponse;
using Domain.DTOs.BookingDTOs;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController(IBookingService bookingService)
{
    [HttpPost]
    public async Task<Response<string>> CreateBookingAsync(CreateBookingDTO dto)
    {
        return await bookingService.CreateBookingAsync(dto);
    }
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteBookingAsync(int id)
    {
        return await bookingService.DeleteBookingAsync(id);
    }
    [HttpGet("{id:int}")]
    public async Task<Response<GetBookingDTO>> GetBookingByIdAsync(int id)
    {
        return await bookingService.GetBookingByIdAsync(id);
    }
    [HttpGet]
    public async Task<Response<List<GetBookingDTO>>> GetBookingsAsync()
    {
        return await bookingService.GetBookingsAsync();
    }
    [HttpPut]
    public async Task<Response<string>> UpdateBookingAsync(UpdateBookingDTO dto)
    {
        return await bookingService.UpdateBookingAsync(dto);
    }
    [HttpGet("GetActiveBookingsAsync")]
    public async Task<Response<List<ActiveBookingDTO>>> GetActiveBookingsAsync()
    {
        return await bookingService.GetActiveBookingsAsync();
    }

}

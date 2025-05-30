using Domain.ApiResponse;
using Domain.DTOs.CarDTOs;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController(ICarService carService)
{
    [HttpPost]
    public async Task<Response<string>> CreateCarAsync(CreateCarDTO dto)
    {
        return await carService.CreateCarAsync(dto);
    }
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteCarAsync(int id)
    {
        return await carService.DeleteCarAsync(id);
    }
    [HttpGet("{id:int}")]
    public async Task<Response<GetCarDTO>> GetCarByIdAsync(int id)
    {
        return await carService.GetCarByIdAsync(id);
    }
    [HttpGet]
    public async Task<Response<List<GetCarDTO>>> GetCarsAsync()
    {
        return await carService.GetCarsAsync();
    }
    [HttpPut]
    public async Task<Response<string>> UpdateCarAsync(UpdateCarDTO dto)
    {
        return await carService.UpdateCarAsync(dto);
    }
    [HttpGet("GetAvailableCarsNowAsync")]
    public async Task<Response<List<AvailableCarDTO>>> GetAvailableCarsNowAsync()
    {
        return await carService.GetAvailableCarsNowAsync();
    }
    [HttpGet("GetMostPopularCarsAsync")]
    public async Task<Response<List<PopularCarDTO>>> GetMostPopularCarsAsync()
    {
        return await carService.GetMostPopularCarsAsync();
    }
    [HttpGet("GetCarBookingDetailsAsync")]
    public async Task<Response<List<CarBookingDetailsDTO>>> GetCarBookingDetailsAsync(int id)
    {
        return await carService.GetCarBookingDetailsAsync(id);
    }

}

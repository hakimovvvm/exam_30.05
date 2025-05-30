using Domain.ApiResponse;
using Domain.DTOs.CarDTOs;

namespace Infrastructure.Interfaces;

public interface ICarService
{
    public Task<Response<List<GetCarDTO>>> GetCarsAsync();
    public Task<Response<GetCarDTO>> GetCarByIdAsync(int id);
    public Task<Response<string>> CreateCarAsync(CreateCarDTO dto);
    public Task<Response<string>> UpdateCarAsync(UpdateCarDTO dto);
    public Task<Response<string>> DeleteCarAsync(int id);
    public Task<Response<List<AvailableCarDTO>>> GetAvailableCarsNowAsync();
    public Task<Response<List<PopularCarDTO>>> GetMostPopularCarsAsync();
    public Task<Response<List<CarBookingDetailsDTO>>> GetCarBookingDetailsAsync(int carId);


}

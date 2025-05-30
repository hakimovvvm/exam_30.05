using Domain.ApiResponse;
using Domain.DTOs.UserDTOs;

namespace Infrastructure.Interfaces;

public interface IUserService
{
    public Task<Response<List<GetUserDTO>>> GetUsersAsync();
    public Task<Response<GetUserDTO>> GetUserByIdAsync(int id);
    public Task<Response<string>> CreateUserAsync(CreateUserDTO dto);
    public Task<Response<string>> UpdateUserAsync(UpdateUserDTO dto);
    public Task<Response<string>> DeleteUserAsync(int id);
    public Task<Response<List<FrequentRenterDTO>>> GetFrequentRentersAsync();

}

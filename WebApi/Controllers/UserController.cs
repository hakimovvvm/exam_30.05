using Domain.ApiResponse;
using Domain.DTOs.UserDTOs;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService)
{
    [HttpPost]
    public async Task<Response<string>> CreateUserAsync(CreateUserDTO dto)
    {
        return await userService.CreateUserAsync(dto);
    }
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteUserAsync(int id)
    {
        return await userService.DeleteUserAsync(id);
    }
    [HttpGet("{id:int}")]
    public async Task<Response<GetUserDTO>> GetUserByIdAsync(int id)
    {
        return await userService.GetUserByIdAsync(id);
    }
    [HttpGet]
    public async Task<Response<List<GetUserDTO>>> GetUsersAsync()
    {
        return await userService.GetUsersAsync();
    }
    [HttpPut]
    public async Task<Response<string>> UpdateUserAsync(UpdateUserDTO dto)
    {
        return await userService.UpdateUserAsync(dto);
    }
    [HttpGet("GetFrequentRenters")]
    public async Task<Response<List<FrequentRenterDTO>>> GetFrequentRentersAsync()
    {
        return await userService.GetFrequentRentersAsync();
    }

}

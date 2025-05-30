using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.CompilerServices;
using System.Xml;
using Domain.ApiResponse;
using Domain.DTOs.UserDTOs;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Infrastructure.Services;

public class UserService(DataContext context) : IUserService
{
    public async Task<Response<string>> CreateUserAsync(CreateUserDTO dto)
    {
        var user = new User
        {
            UserName = dto.UserName,
            Email = dto.Email,
            Phone = dto.Phone
        };

        await context.Users.AddAsync(user);
        var result = await context.SaveChangesAsync();

        return result <= 0
            ? new Response<string>("something went wrong", HttpStatusCode.BadRequest)
            : new Response<string>("user added successfully", null);
    }

    public async Task<Response<string>> DeleteUserAsync(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null)
            return new Response<string>("user not found", HttpStatusCode.NotFound);

        context.Users.Remove(user);
        var result = await context.SaveChangesAsync();

        return result <= 0
            ? new Response<string>("something went wrong", HttpStatusCode.InternalServerError)
            : new Response<string>("user removed successfully", null);
    }

    public async Task<Response<GetUserDTO>> GetUserByIdAsync(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null)
            return new Response<GetUserDTO>("user not found", HttpStatusCode.NotFound);

        var dto = new GetUserDTO
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Phone = user.Phone
        };

        return new Response<GetUserDTO>("success", dto);
    }

    public async Task<Response<List<GetUserDTO>>> GetUsersAsync()
    {
        var users = await context.Users.Select(u => new GetUserDTO
        {
            Id = u.Id,
            UserName = u.UserName,
            Email = u.Email,
            Phone = u.Phone
        }).ToListAsync();

        return users == null
            ? new Response<List<GetUserDTO>>("something went wrong", HttpStatusCode.InternalServerError)
            : new Response<List<GetUserDTO>>("success", users);
    }

    public async Task<Response<string>> UpdateUserAsync(UpdateUserDTO dto)
    {
        var user = await context.Users.FindAsync(dto.Id);
        if (user == null)
            return new Response<string>("user not found", HttpStatusCode.NotFound);

        user.UserName = dto.UserName;
        user.Email = dto.Email;
        user.Phone = dto.Phone;

        var result = await context.SaveChangesAsync();
        return result <= 0
            ? new Response<string>("something went wrong", HttpStatusCode.InternalServerError)
            : new Response<string>("successfully", null);
    }

    public async Task<Response<List<FrequentRenterDTO>>> GetFrequentRentersAsync()
    {
        var users = await context.Users
            .Include(u => u.Bookings)
            .Where(u => u.Bookings.Count > 3)
            .Select(u => new FrequentRenterDTO
            {
                Username = u.UserName,
                BookingCount = u.Bookings.Count
            })
            .ToListAsync();

        return users == null
            ? new Response<List<FrequentRenterDTO>>("something went wrong", HttpStatusCode.NotFound)
            : new Response<List<FrequentRenterDTO>>("successfully", users);
    }

}

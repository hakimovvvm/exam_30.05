using System.Net;
using System.Xml;
using Domain.ApiResponse;
using Domain.DTOs.BookingDTOs;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class BookingService(DataContext context) : IBookingService
{
    public async Task<Response<string>> CreateBookingAsync(CreateBookingDTO dto)
    {
        if (dto.StartDate >= dto.EndDate)
            return new Response<string>("endDate cant be erly", HttpStatusCode.BadRequest);

        var car = await context.Cars
            .Where(c => c.Id == dto.CarId && c.IsAvailable)
            .FirstOrDefaultAsync();

        if (car == null)
            return new Response<string>("car not found or not available", HttpStatusCode.NotFound);

        var totalDays = (dto.EndDate - dto.StartDate).Days;
        if (totalDays <= 0)
            return new Response<string>("an incorrect days entered", HttpStatusCode.BadRequest);

        var booking = new Booking
        {
            UserId = dto.UserId,
            CarId = dto.CarId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            TotalPrice = totalDays * car.PricePerDay
        };

        await context.Bookings.AddAsync(booking);
        var result = await context.SaveChangesAsync();

        return result <= 0
            ? new Response<string>("something went wrong", HttpStatusCode.InternalServerError)
            : new Response<string>("booking added successfully", null);
    }

    public async Task<Response<string>> DeleteBookingAsync(int id)
    {
        var booking = await context.Bookings.FindAsync(id);
        if (booking == null)
            return new Response<string>("Booking not found", HttpStatusCode.NotFound);

        context.Bookings.Remove(booking);
        var result = await context.SaveChangesAsync();

        return result <= 0
            ? new Response<string>("something went wrong", HttpStatusCode.InternalServerError)
            : new Response<string>("Booking remove successfully", null);
    }

    public async Task<Response<GetBookingDTO>> GetBookingByIdAsync(int id)
    {
        var booking = await context.Bookings
            .Include(b => b.User)
            .Include(b => b.Car)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (booking == null)
            return new Response<GetBookingDTO>("booking not found", HttpStatusCode.NotFound);

        var dto = new GetBookingDTO
        {
            Id = booking.Id,
            UserId = booking.UserId,
            UserName = booking.User.UserName,
            CarId = booking.CarId,
            CarModel = booking.Car.Model,
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            TotalPrice = booking.TotalPrice
        };

        return dto == null
            ? new Response<GetBookingDTO>("booking not found", HttpStatusCode.NotFound)
            : new Response<GetBookingDTO>("successfully", dto);
    }

    public async Task<Response<List<GetBookingDTO>>> GetBookingsAsync()
    {
        var bookings = await context.Bookings
             .Include(b => b.User)
             .Include(b => b.Car)
             .Select(b => new GetBookingDTO
             {
                 Id = b.Id,
                 UserId = b.UserId,
                 UserName = b.User.UserName,
                 CarId = b.CarId,
                 CarModel = b.Car.Model,
                 StartDate = b.StartDate,
                 EndDate = b.EndDate,
                 TotalPrice = b.TotalPrice
             })
             .ToListAsync();

        if (bookings == null)
            return new Response<List<GetBookingDTO>>("something went wrong", HttpStatusCode.NotFound);

        return new Response<List<GetBookingDTO>>("successfully", bookings);
    }

    public async Task<Response<string>> UpdateBookingAsync(UpdateBookingDTO dto)
    {


        if (dto.StartDate >= dto.EndDate)
            return new Response<string>("endDate cant be erly", HttpStatusCode.BadRequest);

        var booking = await context.Bookings.FindAsync(dto.Id);
        if (booking == null)
            return new Response<string>("booking not found", HttpStatusCode.NotFound);

        var car = await context.Cars.FindAsync(dto.CarId);
        if (car == null)
            return new Response<string>("Car not found", HttpStatusCode.NotFound);

        var totalDays = (dto.EndDate - dto.StartDate).Days;
        if (totalDays <= 0)
            return new Response<string>("an incorrect days entered", HttpStatusCode.BadRequest);

        booking.UserId = dto.UserId;
        booking.CarId = dto.CarId;
        booking.StartDate = dto.StartDate;
        booking.EndDate = dto.EndDate;
        booking.TotalPrice = totalDays * car.PricePerDay;

        var result = await context.SaveChangesAsync();
        return result <= 0
            ? new Response<string>("something went wrong", HttpStatusCode.InternalServerError)
            : new Response<string>("successfully", null);
    }

    public async Task<Response<List<ActiveBookingDTO>>> GetActiveBookingsAsync()
    {
        var now = DateTime.UtcNow;

        var bookings = await context.Bookings
            .Include(b => b.Car)
            .Include(b => b.User)
            .Where(b => b.StartDate <= now && b.EndDate >= now)
            .Select(b => new ActiveBookingDTO
            {
                CarModel = b.Car.Model,
                Username = b.User.UserName,
                EndDate = b.EndDate
            })
            .ToListAsync();

        return bookings == null
            ? new Response<List<ActiveBookingDTO>>("something went wrong", HttpStatusCode.NotFound)
            : new Response<List<ActiveBookingDTO>>("successfully", bookings);
    }

}

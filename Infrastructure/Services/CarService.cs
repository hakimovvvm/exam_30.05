using System.Net;
using Domain.ApiResponse;
using Domain.DTOs.CarDTOs;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CarService(DataContext context) : ICarService
{
    public async Task<Response<string>> CreateCarAsync(CreateCarDTO dto)
    {
        var car = new Car
        {
            Model = dto.Model,
            PricePerDay = dto.PricePerDay,
            IsAvailable = dto.IsAvailable
        };

        await context.Cars.AddAsync(car);
        var result = await context.SaveChangesAsync();

        return result <= 0
            ? new Response<string>("something went wrong", HttpStatusCode.BadRequest)
            : new Response<string>("Car add successfully", null);
    }

    public async Task<Response<string>> DeleteCarAsync(int id)
    {
        var car = await context.Cars.FindAsync(id);
        if (car == null)
            return new Response<string>("Car not found", HttpStatusCode.NotFound);

        context.Cars.Remove(car);
        var result = await context.SaveChangesAsync();
        return result <= 0
            ? new Response<string>("something went wrong", HttpStatusCode.InternalServerError)
            : new Response<string>("Car remove successfully", null);
    }

    public async Task<Response<GetCarDTO>> GetCarByIdAsync(int id)
    {
        var car = await context.Cars.FindAsync(id);

        if (car == null)
            return new Response<GetCarDTO>("Car not found", HttpStatusCode.NotFound);

        var dto = new GetCarDTO
        {
            Id = car.Id,
            Model = car.Model,
            PricePerDay = car.PricePerDay,
            IsAvailable = car.IsAvailable
        };

        return dto == null
            ? new Response<GetCarDTO>("Car not found", HttpStatusCode.NotFound)
            : new Response<GetCarDTO>("successfully", dto);
    }

    public async Task<Response<List<GetCarDTO>>> GetCarsAsync()
    {
        var cars = await context.Cars.Select(car => new GetCarDTO
        {
            Id = car.Id,
            Model = car.Model,
            PricePerDay = car.PricePerDay,
            IsAvailable = car.IsAvailable
        }).ToListAsync();

        return cars == null
            ? new Response<List<GetCarDTO>>("something went wrong", HttpStatusCode.InternalServerError)
            : new Response<List<GetCarDTO>>("successfully", cars);
    }

    public async Task<Response<string>> UpdateCarAsync(UpdateCarDTO dto)
    {
        var car = await context.Cars.FindAsync(dto.Id);
        if (car == null)
            return new Response<string>("Car not found", HttpStatusCode.NotFound);


        car.Model = dto.Model;
        car.PricePerDay = dto.PricePerDay;
        car.IsAvailable = dto.IsAvailable;

        var result = context.SaveChanges();
        return result <= 0
            ? new Response<string>("something went wrong", HttpStatusCode.InternalServerError)
            : new Response<string>("successfully", null);
    }

    public async Task<Response<List<AvailableCarDTO>>> GetAvailableCarsNowAsync()
    {
        var now = DateTime.Now;
        var cars = await context.Cars
            .Include(c => c.Bookings)
            .Where(car => car.IsAvailable)
            .ToListAsync();

        var available = cars
            .Where(car => !car.Bookings.Any(b => b.StartDate <= now && b.EndDate >= now))
            .Select(car => new AvailableCarDTO
            {
                Model = car.Model,
                PricePerDay = car.PricePerDay
            })
            .ToList();

        return new Response<List<AvailableCarDTO>>("successfully", available);
    }
    public async Task<Response<List<PopularCarDTO>>> GetMostPopularCarsAsync()
    {
        var popularCars = await context.Cars
            .Include(c => c.Bookings)
            .Select(car => new PopularCarDTO
            {
                Model = car.Model,
                BookingCount = car.Bookings.Count
            })
            .OrderByDescending(c => c.BookingCount)
            .Take(3)
            .ToListAsync();

        return popularCars == null
                   ? new Response<List<PopularCarDTO>>("something went wrong", HttpStatusCode.NotFound)
                   : new Response<List<PopularCarDTO>>("successfully", popularCars);
    }

    public async Task<Response<List<CarBookingDetailsDTO>>> GetCarBookingDetailsAsync(int carId)
    {
        var bookings = await context.Bookings
            .Include(b => b.User)
            .Where(b => b.CarId == carId)
            .Select(b => new CarBookingDetailsDTO
            {
                Username = b.User.UserName,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                TotalPrice = b.TotalPrice
            })
            .ToListAsync();


        return bookings == null
                   ? new Response<List<CarBookingDetailsDTO>>("something went wrong", HttpStatusCode.NotFound)
                   : new Response<List<CarBookingDetailsDTO>>("successfully", bookings);
    }

}

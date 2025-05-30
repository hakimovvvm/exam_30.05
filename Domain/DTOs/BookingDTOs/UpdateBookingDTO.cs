using Domain.DTOs.CarDTOs;

namespace Domain.DTOs.BookingDTOs;

public class UpdateBookingDTO : GetBookingDTO
{
    public int Id { get; set; }
}

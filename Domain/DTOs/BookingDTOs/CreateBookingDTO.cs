using Domain.Entities;

namespace Domain.DTOs.BookingDTOs;

public class CreateBookingDTO
{
    public int UserId { get; set; }
    public int CarId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }

}

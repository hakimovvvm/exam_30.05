namespace Domain.DTOs.CarDTOs;

public class CarBookingDetailsDTO
{
    public string Username { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
}

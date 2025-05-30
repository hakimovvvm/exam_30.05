namespace Domain.DTOs.CarDTOs;

public class CreateCarDTO
{
    public string Model { get; set; }
    public decimal PricePerDay { get; set; }
    public bool IsAvailable { get; set; } = true;

}

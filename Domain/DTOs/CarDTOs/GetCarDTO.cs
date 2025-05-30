namespace Domain.DTOs.CarDTOs;

public class GetCarDTO : CreateCarDTO
{
    public int Id { get; set; }
    public int BookingCount { get; set; }
}

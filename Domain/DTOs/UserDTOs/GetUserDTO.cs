namespace Domain.DTOs.UserDTOs;

public class GetUserDTO : CreateUserDTO
{
    public int Id { get; set; }
    public int BookingCount { get; set; }
}

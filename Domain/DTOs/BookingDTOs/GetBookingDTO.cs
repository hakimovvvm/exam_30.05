namespace Domain.DTOs.BookingDTOs;

public class GetBookingDTO : CreateBookingDTO
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string CarModel { get; set; }

}

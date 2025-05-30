using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Car
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "name of model can be longer than 100")]
    public string Model { get; set; }
    [Required]
    [Range(1, 999999)]
    public decimal PricePerDay { get; set; }
    public bool IsAvailable { get; set; } = true;

    // nav
    public List<Booking> Bookings { get; set; }
}

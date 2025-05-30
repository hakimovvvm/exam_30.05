using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Domain.Entities;

public class Booking
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CarId { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }

    // nav
    public User User { get; set; }
    public Car Car { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50, ErrorMessage = "name is very long - (50)")]
    public string UserName { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "email is very long - (50)")]
    public string Email { get; set; }
    [MaxLength(20, ErrorMessage = "phone number is very long - (50)")]
    public string Phone { get; set; }

    //nav
    public List<Booking> Bookings { get; set; }
}

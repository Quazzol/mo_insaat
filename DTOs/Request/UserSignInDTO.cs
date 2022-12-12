using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Request;

public class UserSignInDTO
{
    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Request;

public class UserSignUpDTO
{
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? LanguageCode { get; set; }
}
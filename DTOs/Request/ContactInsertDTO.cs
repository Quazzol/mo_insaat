using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Request;

public class ContactInsertDTO
{
    [Required]
    public string? LanguageCode { get; set; }
    [Required]
    public string? Content { get; set; }
}
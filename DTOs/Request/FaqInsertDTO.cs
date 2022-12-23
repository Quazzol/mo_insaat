using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Request;

public class FaqInsertDTO
{
    [Required]
    public string? LanguageCode { get; set; }
    [Required]
    public string? Question { get; set; }
    [Required]
    public string? Answer { get; set; }
}
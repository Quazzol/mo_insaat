using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Request;

public class LegalInsertDTO
{
    [Required]
    public string? LanguageCode { get; set; }
    [Required]
    public string? Name { get; set; }
    public string? Content { get; set; }
}
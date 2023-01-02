using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Request;

public class LegalUpdateDTO
{
    [Required]
    public Guid Id { get; set; }
    public string? LanguageCode { get; set; }
    public string? Name { get; set; }
    public string? Content { get; set; }
}
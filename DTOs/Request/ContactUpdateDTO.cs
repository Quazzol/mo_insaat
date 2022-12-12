using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Request;

public class ContactUpdateDTO
{
    [Required]
    public Guid Id { get; set; }
    public string? LanguageCode { get; set; }
    public string? Content { get; set; }
    public int SortOrder { get; set; }
}
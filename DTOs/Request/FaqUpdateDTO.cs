using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Request;

public class FaqUpdateDTO
{
    [Required]
    public Guid Id { get; set; }
    public string? LanguageCode { get; set; }
    public string? Question { get; set; }
    public string? Answer { get; set; }
    public int SortOrder { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Request;

public class ContentTitleInsertDTO
{
    [Required]
    public string? LanguageCode { get; set; }
    [Required]
    public string? Name { get; set; }
    public Guid? HeaderContentId { get; set; }
}
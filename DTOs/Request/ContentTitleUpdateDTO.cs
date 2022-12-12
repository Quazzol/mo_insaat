using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Request;

public class ContentTitleUpdateDTO
{
    [Required]
    public Guid Id { get; set; }
    public string? LanguageCode { get; set; }
    public string? Name { get; set; }
    public int SortOrder { get; set; }
    public Guid? HeaderContentId { get; set; }
}
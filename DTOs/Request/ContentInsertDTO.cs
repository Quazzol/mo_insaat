using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Request;

public class ContentInsertDTO
{
    [Required]
    public string? LanguageCode { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Content { get; set; }
    public bool IsSubContent { get; set; }
    public bool IsImageLibrary { get; set; }
    public bool IsVisibleOnIndex { get; set; }
    public bool IsCompleted { get; set; }
    [Required]
    public Guid? HeaderContentId { get; set; }
}
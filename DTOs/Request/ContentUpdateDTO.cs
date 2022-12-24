using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Request;

public class ContentUpdateDTO
{
    [Required]
    public Guid Id { get; set; }
    public string? LanguageCode { get; set; }
    public string? Name { get; set; }
    public string? Content { get; set; }
    public bool? IsSubContent { get; set; }
    public bool? IsImageLibrary { get; set; }
    public bool? IsVisibleOnIndex { get; set; }
    public bool? IsCompleted { get; set; }
    public int SortOrder { get; set; }
}
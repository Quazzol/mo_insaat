using System.ComponentModel.DataAnnotations;
using Backend.Datas.Enums;

namespace Backend.DTOs.Request;

public class ContentInsertDTO
{
    [Required]
    public string? LanguageCode { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public ContentType Type { get; set; }
    [Required]
    public string? Content { get; set; }
    public bool? ImageLibrary { get; set; }
    public bool? VisibleOnMain { get; set; }
    public bool? IsFixed { get; set; }
    [Required]
    public Guid? HeaderContentId { get; set; }
}
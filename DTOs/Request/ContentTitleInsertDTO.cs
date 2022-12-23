using System.ComponentModel.DataAnnotations;
using Backend.Datas.Enums;

namespace Backend.DTOs.Request;

public class ContentTitleInsertDTO
{
    [Required]
    public string? LanguageCode { get; set; }
    [Required]
    public ContentType Type { get; set; }
    [Required]
    public string? Name { get; set; }
    public bool? IsFixed { get; set; }
    public Guid? HeaderContentId { get; set; }
}
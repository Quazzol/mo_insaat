using System.ComponentModel.DataAnnotations;
using Backend.Datas.Enums;

namespace Backend.DTOs.Request;

public class LegalInsertDTO
{
    [Required]
    public string? LanguageCode { get; set; }
    [Required]
    public string? Name { get; set; }
    public string? Content { get; set; }
}
using System.ComponentModel.DataAnnotations;
using Backend.Datas.Enums;

namespace Backend.DTOs.Request;

public class CompanyInfoInsertDTO
{
    [Required]
    public string? LanguageCode { get; set; }
    [Required]
    public CompanyInfoType Type { get; set; }
    [Required]
    public string? Content { get; set; }
}
using System.ComponentModel.DataAnnotations;
using Backend.Datas.Enums;

namespace Backend.DTOs.Request;

public class CompanyInfoUpdateDTO
{
    [Required]
    public Guid Id { get; set; }
    public string? LanguageCode { get; set; }
    public CompanyInfoType Type { get; set; }
    public string? Content { get; set; }
}
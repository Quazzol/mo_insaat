using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Datas.Enums;

namespace Backend.Models;

[Table("company_info")]
public class CompanyInfoModel
{
    [Key]
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string? LanguageCode { get; set; }

    [Required]
    public CompanyInfoType Type { get; set; }

    [Required]
    public string? Name { get; set; }

    public string? Content { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Datas.Enums;

namespace Backend.Models;

[Table("legal")]
public class LegalModel
{
    [Key]
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string? LanguageCode { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Link { get; set; }

    public string? Content { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("faq")]
public class FaqModel
{
    [Key]
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string? LanguageCode { get; set; }

    [Required]
    public string? Question { get; set; }

    [Required]
    public string? Answer { get; set; }

    public int SortOrder { get; set; }
}
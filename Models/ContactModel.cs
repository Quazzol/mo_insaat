using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("contact")]
public class ContactModel
{
    [Key]
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string? LanguageCode { get; set; }

    public string? Content { get; set; }

    public int SortOrder { get; set; }
}
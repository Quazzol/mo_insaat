using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("image_library")]
public class ImageLibraryModel
{
    [Key]
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string? Name { get; set; }

    public int SortOrder { get; set; }

    public bool IsCover { get; set; }

    public Guid ContentId { get; set; }

    [ForeignKey("ContentId")]
    public ContentModel? Content { get; set; }
}
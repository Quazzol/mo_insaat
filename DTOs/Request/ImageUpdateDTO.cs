using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Request;

public class ImageUpdateDTO
{
    [Required]
    public Guid Id { get; set; }
    public int SortOrder { get; set; }
    public bool IsCover { get; set; }
}
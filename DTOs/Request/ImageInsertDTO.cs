using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Request;

public class ImageInsertDTO
{
    [Required]
    public Guid ContentId { get; set; }
    public bool IsCover { get; set; }
    [Required]
    public IFormFile? Image { get; set; }
}
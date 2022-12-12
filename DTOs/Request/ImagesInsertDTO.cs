using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Request;

public class ImagesInsertDTO
{
    [Required]
    public Guid ContentId { get; set; }
    [Required]
    public IList<IFormFile>? Image { get; set; }
}
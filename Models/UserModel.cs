using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Datas.Enums;

namespace Backend.Models;

[Table("user")]
public class UserModel
{
    [Key]
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    public UserType Type { get; set; }

    [Required]
    public string? LanguageCode { get; set; }
}
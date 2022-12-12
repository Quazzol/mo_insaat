using System.ComponentModel.DataAnnotations;
using Backend.Datas.Enums;

namespace Backend.DTOs.Request;

public class UserUpdateDTO
{
    [Required]
    public Guid Id { get; set; }
    public string? CurrentPassword { get; set; }
    public string? Password { get; set; }
    public UserType Type { get; set; }
}
using System.ComponentModel.DataAnnotations;
using Backend.Datas.Enums;

namespace Backend.DTOs.Response;

public class UserSignInResultDTO
{
    [Required]
    public string? Token { get; set; }

    [Required]
    public DateTime ValidTo { get; set; }

    [Required]
    public SignInResultType ResultType { get; set; }
}
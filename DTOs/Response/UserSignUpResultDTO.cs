using System.ComponentModel.DataAnnotations;
using Backend.Datas.Enums;

namespace Backend.DTOs.Response;

public class UserSignUpResultDTO
{
    public string? Password { get; set; }
    [Required]
    public SignUpResultType ResultType { get; set; }
}
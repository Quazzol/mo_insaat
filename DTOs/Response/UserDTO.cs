using Backend.Datas.Enums;

namespace Backend.DTOs.Response;

public class UserDTO
{
    public Guid Id { get; set; }
    public UserType Type { get; set; }
    public string? LanguageCode { get; set; }
}
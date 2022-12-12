using Backend.DTOs.Response;

namespace Backend.DTOs;

public class UserContext : IUserContext
{
    public UserDTO? CurrentUser { get; set; }
}
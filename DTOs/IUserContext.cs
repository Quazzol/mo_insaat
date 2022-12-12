using Backend.DTOs.Response;

namespace Backend.DTOs;

public interface IUserContext
{
    public UserDTO? CurrentUser { get; }
}
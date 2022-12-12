using Backend.DTOs.Request;
using Backend.DTOs.Response;
using Backend.Datas.Enums;

namespace Backend.Services.Interfaces;

public interface IUserService
{
    public UserSignInResultDTO SignIn(UserSignInDTO user);
    public UserSignUpResultDTO SignUp(UserSignUpDTO user);
    public UserUpdateResult UpdateUser(UserUpdateDTO user);
    public UserUpdateResult ValidatePendingUser(UserUpdateDTO user);
}
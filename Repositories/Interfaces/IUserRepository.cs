using Backend.Datas.Enums;
using Backend.DTOs.Request;
using Backend.Models;

namespace Backend.Repositories.Interfaces;

public interface IUserRepository
{
    public (UserModel? model, SignInResultType resultType) SignIn(UserSignInDTO user);
    public (string password, SignUpResultType resultType) SignUp(UserSignUpDTO user);
    public UserUpdateResult UpdateUser(UserUpdateDTO user);
    public UserUpdateResult ValidatePendingUser(UserUpdateDTO user);
}
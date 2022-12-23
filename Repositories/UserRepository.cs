using Backend.Connection;
using Backend.Datas.Enums;
using Backend.DTOs;
using Backend.DTOs.Request;
using Backend.Misc;
using Backend.Models;
using Backend.Repositories.Interfaces;

namespace Backend.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly IUserContext _userContext;

    public UserRepository(AppDbContext context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public (UserModel? model, SignInResultType resultType) SignIn(UserSignInDTO user)
    {
        var foundUser = _context.Users?.FirstOrDefault<UserModel>(q => q.Username == user.Username && q.Password == AesEncryption.Encrypt(user.Password));
        var loginResult = SignInResultType.Success;
        if (foundUser is null)
        {
            loginResult = SignInResultType.InvalidUserNameOrPassword;
        }
        else if (foundUser.Type == UserType.Pending)
        {
            loginResult = SignInResultType.Pending;
        }
        else if (foundUser.Type == UserType.Blocked)
        {
            loginResult = SignInResultType.Blocked;
        }
        return (foundUser, loginResult);
    }

    public (string password, SignUpResultType resultType) SignUp(UserSignUpDTO user)
    {
        var password = CreateRandomPassword();
        var model = new UserModel()
        {
            Id = Guid.NewGuid(),
            Username = user.Username,
            Password = AesEncryption.Encrypt(password),
            Email = user.Email,
            Type = UserType.Pending,
            LanguageCode = user.LanguageCode
        };

        if (!IsValidEmail(user.Email))
            return (string.Empty, SignUpResultType.IncorrectEmail);

        var foundUser = _context.Users?.FirstOrDefault<UserModel>(q => q.Username == user.Username || q.Email == user.Email);
        if (foundUser != null)
            return (string.Empty, SignUpResultType.ExistingUser);

        _context.Users?.Add(model);
        _context.SaveChanges();

        return (password, SignUpResultType.Success);

        bool IsValidEmail(string? email)
        {
            if (email is null)
                return false;

            var trimmedEmail = email.Trim();
            if (trimmedEmail.EndsWith("."))
                return false;
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }

    public UserUpdateResult UpdateUser(UserUpdateDTO user)
    {
        UserModel? foundUser;
        if (_userContext.CurrentUser?.Type == UserType.Admin)
        {
            foundUser = _context.Users?.FirstOrDefault<UserModel>(q => q.Id == user.Id);
        }
        else
        {
            foundUser = _context.Users?.FirstOrDefault<UserModel>(q => q.Id == user.Id && q.Password == AesEncryption.Encrypt(user.CurrentPassword));
        }

        if (foundUser is null)
            return UserUpdateResult.UserNotExists;

        if (_userContext.CurrentUser?.Id == foundUser.Id && user.Password != string.Empty)
        {
            if (!IsValidPassword(user.Password))
                return UserUpdateResult.WeakPassword;

            foundUser.Password = AesEncryption.Encrypt(user.Password);
        }

        if (_userContext.CurrentUser?.Type == UserType.Admin)
        {
            foundUser.Type = user.Type;
        }

        _context.SaveChanges();

        return UserUpdateResult.Success;

        bool IsValidPassword(string? password)
        {
            return !password.IsEmpty() && password!.Length > 8;
        }
    }

    public UserUpdateResult ValidatePendingUser(UserUpdateDTO user)
    {
        var foundUser = _context.Users?.FirstOrDefault<UserModel>(q => q.Id == user.Id && q.Password == AesEncryption.Encrypt(user.CurrentPassword));
        if (foundUser is null)
            return UserUpdateResult.UserNotExists;

        if (foundUser.Type != UserType.Pending)
            return UserUpdateResult.UserNotExists;

        user.Type = UserType.User;
        return UpdateUser(user);
    }

    private string CreateRandomPassword()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
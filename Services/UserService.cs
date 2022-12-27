using Backend.Datas.Enums;
using Backend.DTOs.Request;
using Backend.DTOs.Response;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;

namespace Backend.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly ITokenProviderService _tokenService;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository repository, ITokenProviderService tokenService, ILogger<UserService> logger)
    {
        _repository = repository;
        _tokenService = tokenService;
        _logger = logger;
    }

    public UserSignInResultDTO SignIn(UserSignInDTO user)
    {
        var result = _repository.SignIn(user);

        if (result.resultType != SignInResultType.Success)
        {
            return new UserSignInResultDTO()
            {
                Token = string.Empty,
                ValidTo = DateTime.Now,
                ResultType = result.resultType
            };
        }

        var token = _tokenService.CreateToken(result.model);

        return new UserSignInResultDTO()
        {
            Token = token.token,
            ValidTo = token.validTo,
            ResultType = result.resultType
        };
    }

    public UserSignUpResultDTO SignUp(UserSignUpDTO user)
    {
        var result = _repository.SignUp(user);

        return new UserSignUpResultDTO()
        {
            Password = result.password,
            ResultType = result.resultType
        };
    }

    public UserUpdateResult UpdateUser(UserUpdateDTO user)
    {
        return _repository.UpdateUser(user);
    }

    public UserUpdateResult ValidatePendingUser(UserUpdateDTO user)
    {
        return _repository.ValidatePendingUser(user);
    }
}
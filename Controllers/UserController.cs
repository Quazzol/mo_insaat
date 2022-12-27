using Backend.Authorization;
using Backend.Datas.Enums;
using Backend.DTOs.Request;
using Backend.Misc;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpPost("sign-in")]
    public IActionResult SignIn(UserSignInDTO user)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var result = _service.SignIn(user);

        if (result.ResultType != SignInResultType.Success)
        {
            return SignInFailed(result.ResultType, $"SignIn failed for user {user.Username}!");
        }

        return Ok(result);

        IActionResult SignInFailed(SignInResultType reason, string message)
        {
            return Unauthorized(new ProblemDetails
            {
                Type = "signin-failed",
                Detail = message,
                Status = 401,
                Title = "SignIn Failed",
                Extensions = { { "reason", reason }, { "reasonDescription", reason.GetDescription() } },
                Instance = HttpContext.Request.GetDisplayUrl()
            });
        }
    }

    [Authorize(Policy = Policies.OnlyAdmins)]
    [HttpPost("sign-up")]
    public IActionResult SignUp(UserSignUpDTO user)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var result = _service.SignUp(user);
        if (result.ResultType != SignUpResultType.Success)
        {
            return SignUpFailed(result.ResultType, $"SignUp failed for user {user.Username}!");
        }

        return Ok(result);

        IActionResult SignUpFailed(SignUpResultType reason, string message)
        {
            return Unauthorized(new ProblemDetails
            {
                Type = "signup-failed",
                Detail = message,
                Status = 401,
                Title = "SignUp Failed",
                Extensions = { { "reason", reason }, { "reasonDescription", reason.GetDescription() } },
                Instance = HttpContext.Request.GetDisplayUrl()
            });
        }
    }

    [Authorize(Policy = Policies.OnlyAdmins)]
    [HttpPost("update-user")]
    public IActionResult UpdateUser(UserUpdateDTO user)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var result = _service.UpdateUser(user);
        if (result != UserUpdateResult.Success)
        {
            return UpdateFailed(result, result.GetDescription());
        }

        return Ok();

        IActionResult UpdateFailed(UserUpdateResult reason, string message)
        {
            return Unauthorized(new ProblemDetails
            {
                Type = "update-failed",
                Detail = message,
                Status = 401,
                Title = "User Updated Failed",
                Extensions = { { "reason", reason }, { "reasonDescription", reason.GetDescription() } },
                Instance = HttpContext.Request.GetDisplayUrl()
            });
        }
    }

    [HttpPost("confirm-user")]
    public IActionResult ConfirmUser(UserUpdateDTO user)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        return Ok(_service.ValidatePendingUser(user));
    }
}
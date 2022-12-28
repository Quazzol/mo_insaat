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
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IContentService _contentService;
    private readonly ICompanyInfoService _companyInfoService;
    private readonly IImageLibraryService _imageService;
    private readonly IFaqService _faqService;
    private readonly ILegalService _legalService;

    public AdminController(IUserService userService,
                        IContentService contentService,
                        ICompanyInfoService companyInfoService,
                        IImageLibraryService imageService,
                        IFaqService faqService,
                        ILegalService legalService)
    {
        _userService = userService;
        _contentService = contentService;
        _companyInfoService = companyInfoService;
        _imageService = imageService;
        _faqService = faqService;
        _legalService = legalService;
    }

    #region User

    [HttpPost("sign-in")]
    public IActionResult SignIn(UserSignInDTO user)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var result = _userService.SignIn(user);

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

        var result = _userService.SignUp(user);
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

        var result = _userService.UpdateUser(user);
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

        return Ok(_userService.ValidatePendingUser(user));
    }

    #endregion

    #region MenuItem

    [Authorize(Policy = Policies.AtLeastModerators)]
    [HttpPost("insert-menu-item")]
    public async Task<IActionResult> InsertMenuItem(ContentTitleInsertDTO dto)
    {
        return Ok(await _contentService.InsertContentTitle(dto));
    }

    [Authorize(Policy = Policies.AtLeastModerators)]
    [HttpPost("update-menu-item")]
    public async Task<IActionResult> UpdateMenuItem(ContentTitleUpdateDTO dto)
    {
        return Ok(await _contentService.UpdateContentTitle(dto));
    }

    [Authorize(Policy = Policies.OnlyAdmins)]
    [HttpGet("delete-menu-item")]
    public async Task<IActionResult> DeleteMenuItem(Guid id)
    {
        await _contentService.Delete(id);
        return Ok();
    }

    #endregion

    #region Content

    [Authorize(Policy = Policies.AtLeastModerators)]
    [HttpPost("insert-content")]
    public async Task<IActionResult> InsertContent(ContentInsertDTO dto)
    {
        return Ok(await _contentService.Insert(dto));
    }

    [Authorize(Policy = Policies.AtLeastModerators)]
    [HttpPost("update-content")]
    public async Task<IActionResult> UpdateContent(ContentUpdateDTO dto)
    {
        return Ok(await _contentService.Update(dto));
    }

    [Authorize(Policy = Policies.OnlyAdmins)]
    [HttpGet("delete-content")]
    public async Task<IActionResult> DeleteContent(Guid id)
    {
        await _contentService.Delete(id);
        return Ok();
    }

    #endregion

    #region CompanyInfo

    [Authorize(Policy = Policies.AtLeastModerators)]
    [HttpPost("update-company-info")]
    public async Task<IActionResult> UpdateCompanyInfo(CompanyInfoUpdateDTO dto)
    {
        return Ok(await _companyInfoService.Update(dto));
    }

    #endregion

    #region Image

    [Authorize(Policy = Policies.AtLeastModerators)]
    [HttpPost("insert-image")]
    public async Task<IActionResult> InsertImage([FromForm] ImageInsertDTO dto)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);
        return Ok(await _imageService.InsertImage(dto));
    }

    [Authorize(Policy = Policies.AtLeastModerators)]
    [HttpPost("insert-images")]
    public async Task<IActionResult> InsertImages([FromForm] ImagesInsertDTO dto)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);
        return Ok(await _imageService.InsertMultipleImages(dto));
    }

    [Authorize(Policy = Policies.AtLeastModerators)]
    [HttpPost("update-image")]
    public async Task<IActionResult> UpdateImage(ImageUpdateDTO dto)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);
        return Ok(await _imageService.UpdateImage(dto));
    }

    [Authorize(Policy = Policies.OnlyAdmins)]
    [HttpGet("delete-images")]
    public async Task<IActionResult> DeleteImage([FromQuery(Name = "id")] IEnumerable<Guid> ids)
    {
        await _imageService.DeleteImages(ids);
        return Ok();
    }

    #endregion

    #region Faq

    [Authorize(Policy = Policies.AtLeastModerators)]
    [HttpPost("insert-faq")]
    public async Task<IActionResult> InsertFaq(FaqInsertDTO dto)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);
        return Ok(await _faqService.Insert(dto));
    }

    [Authorize(Policy = Policies.AtLeastModerators)]
    [HttpPost("update-faq")]
    public async Task<IActionResult> UpdateFaq(FaqUpdateDTO dto)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);
        return Ok(await _faqService.Update(dto));
    }

    [Authorize(Policy = Policies.OnlyAdmins)]
    [HttpGet("delete-faq")]
    public async Task<IActionResult> DeleteFaq(Guid id)
    {
        await _faqService.Delete(id);
        return Ok();
    }

    #endregion

    #region Legal

    [Authorize(Policy = Policies.AtLeastModerators)]
    [HttpPost("update-legal")]
    public async Task<IActionResult> UpdateLegal(LegalUpdateDTO dto)
    {
        return Ok(await _legalService.Update(dto));
    }

    #endregion

}
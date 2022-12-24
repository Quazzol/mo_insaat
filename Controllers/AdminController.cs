using Backend.Authorization;
using Backend.DTOs.Request;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IContentService _contentService;
    private readonly ICompanyInfoService _companyInfoService;
    private readonly IImageLibraryService _imageService;
    private readonly IFaqService _faqService;
    private readonly ILegalService _legalService;

    public AdminController(IContentService contentService, ICompanyInfoService companyInfoService, IImageLibraryService imageService, IFaqService faqService, ILegalService legalService)
    {
        _contentService = contentService;
        _companyInfoService = companyInfoService;
        _imageService = imageService;
        _faqService = faqService;
        _legalService = legalService;
    }

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
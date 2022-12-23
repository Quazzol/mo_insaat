using Backend.Datas.Enums;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UIController : ControllerBase
{
    private readonly IContentService _contentService;
    private readonly ICompanyInfoService _companyInfoService;
    private readonly IImageLibraryService _imageLibraryService;
    private readonly IFaqService _faqService;
    private readonly ILegalService _legalService;

    public UIController(IContentService contentService, ICompanyInfoService companyInfoService, IImageLibraryService imageLibraryService, IFaqService faqService, ILegalService legalService)
    {
        _contentService = contentService;
        _companyInfoService = companyInfoService;
        _imageLibraryService = imageLibraryService;
        _faqService = faqService;
        _legalService = legalService;
    }

    [HttpGet("menu")]
    public async Task<IActionResult> GetMenu(string languageCode)
    {
        return Ok(await _contentService.GetAllTitle(languageCode));
    }

    [HttpGet("content")]
    public async Task<IActionResult> GetContentPage(Guid contentId)
    {
        return Ok(await _contentService.Get(contentId));
    }

    [HttpGet("content-by-type")]
    public async Task<IActionResult> GetContent(string languageCode, ContentType type)
    {
        return Ok(await _contentService.GetAll(languageCode, type));
    }

    [HttpGet("main-page")]
    public async Task<IActionResult> GetMainPage(string languageCode)
    {
        return Ok(await _contentService.GetVisibleOnMainPage(languageCode));
    }

    [HttpGet("images")]
    public async Task<IActionResult> GetImages(Guid contentId)
    {
        return Ok(await _imageLibraryService.GetImages(contentId));
    }

    [HttpGet("cover-images")]
    public async Task<IActionResult> GetCoverImages(int count = int.MaxValue)
    {
        return Ok(await _imageLibraryService.GetCoverImages(count));
    }

    [HttpGet("faqs")]
    public async Task<IActionResult> GetFaqs(string languageCode, int count = int.MaxValue)
    {
        return Ok(await _faqService.GetAll(languageCode, count));
    }

    [HttpGet("company-info")]
    public async Task<IActionResult> GetCompanyInfos(string languageCode)
    {
        return Ok(await _companyInfoService.GetAll(languageCode));
    }

    [HttpGet("legal")]
    public async Task<IActionResult> GetLegal(string languageCode)
    {
        return Ok(await _legalService.GetAll(languageCode));
    }
}
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
    public async Task<IActionResult> GetContent(Guid contentId)
    {
        return Ok(await _contentService.Get(contentId));
    }

    [HttpGet("content-by-header")]
    public async Task<IActionResult> GetContentAll(Guid contentHeaderId)
    {
        return Ok(await _contentService.GetAll(contentHeaderId));
    }

    [HttpGet("content-all")]
    public async Task<IActionResult> GetContentAll(string languageCode, int page = 0, int count = int.MaxValue)
    {
        return Ok(await _contentService.GetAll(languageCode, page, count));
    }

    [HttpGet("visible-on-index")]
    public async Task<IActionResult> GetVisibleOnIndex(string languageCode)
    {
        return Ok(await _contentService.GetVisibleOnIndex(languageCode));
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

    [HttpGet("faq")]
    public async Task<IActionResult> GetFaq(Guid id)
    {
        return Ok(await _faqService.Get(id));
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
    public async Task<IActionResult> GetLegal(Guid id)
    {
        return Ok(await _legalService.Get(id));
    }

    [HttpGet("legals")]
    public async Task<IActionResult> GetLegals(string languageCode)
    {
        return Ok(await _legalService.GetAll(languageCode));
    }
}
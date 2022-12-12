using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UIController : ControllerBase
{
    private readonly IContentService _contentService;
    private readonly IContactService _contactService;
    private readonly IImageLibraryService _imageLibraryService;

    public UIController(IContentService contentService, IContactService contactService, IImageLibraryService imageLibraryService)
    {
        _contentService = contentService;
        _contactService = contactService;
        _imageLibraryService = imageLibraryService;
    }

    [HttpGet("menu")]
    public async Task<IActionResult> GetMenu(string languageCode)
    {
        return Ok(await _contentService.GetAllTitle(languageCode));
    }

    [HttpGet("main-page")]
    public async Task<IActionResult> GetMainPageContent(string languageCode)
    {
        return Ok(await _contentService.GetVisibleOnMainPage(languageCode));
    }

    [HttpGet("content")]
    public async Task<IActionResult> GetContentPage(Guid contentId)
    {
        return Ok(await _contentService.Get(contentId));
    }

    [HttpGet("images")]
    public async Task<IActionResult> GetImages(Guid contentId)
    {
        return Ok(await _imageLibraryService.GetImages(contentId));
    }

    [HttpGet("contact-page")]
    public async Task<IActionResult> GetContactPage(Guid id)
    {
        return Ok(await _contentService.Get(id));
    }

    [HttpGet("cover-images")]
    public async Task<IActionResult> GetCoverImages()
    {
        return Ok(await _imageLibraryService.GetCoverImages());
    }
}
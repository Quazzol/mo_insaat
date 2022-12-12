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
    private readonly IContactService _contactService;
    private readonly IImageLibraryService _imageService;

    public AdminController(IContentService contentService, IContactService contactService, IImageLibraryService imageService)
    {
        _contentService = contentService;
        _contactService = contactService;
        _imageService = imageService;
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

    #region Contact

    [Authorize(Policy = Policies.AtLeastModerators)]
    [HttpPost("insert-contact")]
    public async Task<IActionResult> InsertContact(ContactInsertDTO dto)
    {
        return Ok(await _contactService.Insert(dto));
    }

    [Authorize(Policy = Policies.AtLeastModerators)]
    [HttpPost("update-contact")]
    public async Task<IActionResult> UpdateContact(ContactUpdateDTO dto)
    {
        return Ok(await _contactService.Update(dto));
    }

    [Authorize(Policy = Policies.OnlyAdmins)]
    [HttpGet("delete-contact")]
    public async Task<IActionResult> DeleteContact(Guid id)
    {
        await _contactService.Delete(id);
        return Ok();
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
}
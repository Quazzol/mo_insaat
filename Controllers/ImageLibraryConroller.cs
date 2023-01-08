using Backend.Authorization;
using Backend.DTOs.Request;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageLibraryController : ControllerBase
{
    private readonly IImageLibraryService _service;

    public ImageLibraryController(IImageLibraryService service)
    {
        _service = service;
    }

    [HttpGet("get-image")]
    public async Task<IActionResult> GetImage(Guid id)
    {
        return Ok(await _service.GetImage(id));
    }

    [HttpGet("get-images")]
    public async Task<IActionResult> GetImages(Guid contentId)
    {
        return Ok(await _service.GetImages(contentId));
    }

    [HttpGet("get-cover-images")]
    public async Task<IActionResult> GetCoverImages(int count = int.MaxValue)
    {
        return Ok(await _service.GetCoverImages(count));
    }

    [Authorize(Policy = Policies.AtLeastModerators)]
    [HttpPost("save-image")]
    public async Task<IActionResult> SaveImage(ImageInsertDTO image)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);
        return Ok(await _service.InsertImage(image));
    }

    [Authorize(Policy = Policies.AtLeastModerators)]
    [HttpPost("save-images")]
    public async Task<IActionResult> SaveImages(ImagesInsertDTO images)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);
        return Ok(await _service.InsertMultipleImages(images));
    }

    [Authorize(Policy = Policies.OnlyAdmins)]
    [HttpPost("delete-image")]
    public async Task<IActionResult> DeleteImages([FromQuery(Name = "id")] IEnumerable<Guid> ids)
    {
        return Ok(await _service.DeleteImages(ids));
    }

    [Authorize(Policy = Policies.OnlyAdmins)]
    [HttpPost("delete-images-by-content-id")]
    public async Task<IActionResult> DeleteImagesByContentId(Guid id)
    {
        return Ok(await _service.DeleteImagesByContentId(id));
    }
}
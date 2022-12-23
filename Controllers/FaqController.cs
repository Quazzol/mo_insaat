using Backend.Authorization;
using Backend.DTOs.Request;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FaqController : ControllerBase
{
    private readonly IFaqService _service;

    public FaqController(IFaqService service)
    {
        _service = service;
    }

    [HttpGet("get")]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _service.Get(id));
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll(string languageCode, int count = int.MaxValue)
    {
        return Ok(await _service.GetAll(languageCode, count));
    }

    [Authorize(Policy = Policies.AtLeastModerators)]
    [HttpPost("insert")]
    public async Task<IActionResult> Insert(FaqInsertDTO content)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);
        return Ok(await _service.Insert(content));
    }

    [Authorize(Policy = Policies.AtLeastModerators)]
    [HttpPost("update")]
    public async Task<IActionResult> Update(FaqUpdateDTO content)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);
        return Ok(await _service.Update(content));
    }

    [Authorize(Policy = Policies.OnlyAdmins)]
    [HttpGet("delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.Delete(id);
        return Ok();
    }
}
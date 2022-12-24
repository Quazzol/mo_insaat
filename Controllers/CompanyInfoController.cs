using Backend.Authorization;
using Backend.DTOs.Request;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyInfoController : ControllerBase
{
    private readonly ICompanyInfoService _service;

    public CompanyInfoController(ICompanyInfoService service)
    {
        _service = service;
    }

    [HttpGet("get")]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _service.Get(id));
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll(string languageCode)
    {
        return Ok(await _service.GetAll(languageCode));
    }

    [Authorize(Policy = Policies.AtLeastModerators)]
    [HttpPost("update")]
    public async Task<IActionResult> Update(CompanyInfoUpdateDTO content)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);
        return Ok(await _service.Update(content));
    }
}
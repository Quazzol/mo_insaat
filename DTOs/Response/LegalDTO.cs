using Backend.Datas.Enums;

namespace Backend.DTOs.Response;

public class LegalDTO
{
    public Guid Id { get; set; }
    public string? LanguageCode { get; set; }
    public string? Name { get; set; }
    public string? Link { get; set; }
    public string? Content { get; set; }
}
using Backend.Datas.Enums;

namespace Backend.DTOs.Response;

public class CompanyInfoDTO
{
    public Guid Id { get; set; }
    public string? LanguageCode { get; set; }
    public CompanyInfoType Type { get; set; }
    public string? Name { get; set; }
    public string? Content { get; set; }
}
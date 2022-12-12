namespace Backend.DTOs.Response;

public class ContactDTO
{
    public Guid Id { get; set; }
    public string? LanguageCode { get; set; }
    public string? Content { get; set; }
    public int SortOrder { get; set; }
}
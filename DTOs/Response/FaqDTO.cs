namespace Backend.DTOs.Response;

public class FaqDTO
{
    public Guid Id { get; set; }
    public string? LanguageCode { get; set; }
    public string? Question { get; set; }
    public string? Answer { get; set; }
    public int SortOrder { get; set; }
    public Guid? HeaderContentId { get; set; }
}
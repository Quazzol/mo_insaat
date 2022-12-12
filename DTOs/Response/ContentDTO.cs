namespace Backend.DTOs.Response;

public class ContentDTO
{
    public Guid Id { get; set; }
    public string? LanguageCode { get; set; }
    public string? Name { get; set; }
    public string? Content { get; set; }
    public bool ImageLibrary { get; set; }
    public bool VisibleOnMain { get; set; }
    public int SortOrder { get; set; }
    public Guid? HeaderContentId { get; set; }
}
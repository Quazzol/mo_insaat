namespace Backend.DTOs.Response;

public class ContentDTO
{
    public Guid Id { get; set; }
    public string? LanguageCode { get; set; }
    public string? Name { get; set; }
    public string? Content { get; set; }
    public bool IsSubContent { get; set; }
    public bool IsImageLibrary { get; set; }
    public bool IsVisibleOnIndex { get; set; }
    public bool IsFixed { get; set; }
    public bool IsCompleted { get; set; }
    public int SortOrder { get; set; }
    public Guid? HeaderContentId { get; set; }
    public ContentDTO? HeaderContent { get; set; }
    public ICollection<ImageDTO>? Images { get; set; }
}
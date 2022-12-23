using Backend.Datas.Enums;

namespace Backend.DTOs.Response;

public class ContentTitleDTO
{
    public Guid Id { get; set; }
    public ContentType Type { get; set; }
    public string? Name { get; set; }
    public string? Link { get; set; }
    public int SortOrder { get; set; }
    public bool IsFixed { get; set; }
    public Guid? HeaderContentId { get; set; }
}
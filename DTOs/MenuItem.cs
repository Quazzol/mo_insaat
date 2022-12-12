using Backend.DTOs.Response;

namespace Backend.DTOs;

public class MenuItem
{
    public ContentTitleDTO? Title { get; set; }
    public List<MenuItem>? SubTitles { get; set; }
}
namespace Backend.Services.Interfaces;

public interface IImageService
{
    public Task<(string, long)> SaveImage(IFormFile? image, string path);
    public bool DeleteImage(string name);
    public bool DeleteFolder(string name);
}
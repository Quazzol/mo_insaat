using Backend.DTOs.Request;
using Backend.Models;

namespace Backend.Repositories.Interfaces;

public interface IImageLibraryRepository
{
    public Task<ImageLibraryModel?> GetImage(Guid id);
    public Task<IEnumerable<ImageLibraryModel?>> GetImages(Guid contentId);
    public Task<IEnumerable<ImageLibraryModel?>> GetCoverImages();
    public Task<string?> InsertImage(ImageInsertDTO image, string name);
    public Task<string?> UpdateImage(ImageUpdateDTO image);
    public Task<bool> DeleteImage(Guid id);
}
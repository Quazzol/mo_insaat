using Backend.DTOs.Request;
using Backend.DTOs.Response;

namespace Backend.Services.Interfaces;

public interface IImageLibraryService
{
    public Task<ImageDTO?> GetImage(Guid id);
    public Task<IEnumerable<ImageDTO?>> GetImages(Guid contentId);
    public Task<IEnumerable<ImageDTO?>> GetCoverImages(int count);
    public Task<string?> InsertImage(ImageInsertDTO image);
    public Task<IEnumerable<string?>> InsertMultipleImages(ImagesInsertDTO images);
    public Task<string?> UpdateImage(ImageUpdateDTO image);
    public Task<bool> DeleteImages(IEnumerable<Guid> ids);
}
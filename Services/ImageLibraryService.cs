using AutoMapper;
using Backend.DTOs.Request;
using Backend.DTOs.Response;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;

namespace Backend.Services;

public class ImageLibraryService : IImageLibraryService
{
    private readonly IImageService _imageService;
    private readonly IImageLibraryRepository _repository;
    private readonly IMapper _mapper;

    public ImageLibraryService(IImageService imageService, IImageLibraryRepository repository, IMapper mapper)
    {
        _imageService = imageService;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ImageDTO?> GetImage(Guid id)
    {
        return _mapper.Map<ImageDTO?>(await _repository.GetImage(id));
    }

    public async Task<IEnumerable<ImageDTO?>> GetImages(Guid contentId)
    {
        return _mapper.Map<IEnumerable<ImageDTO?>>(await _repository.GetImages(contentId));
    }

    public async Task<IEnumerable<ImageDTO?>> GetCoverImages(int count)
    {
        return _mapper.Map<IEnumerable<ImageDTO?>>(await _repository.GetCoverImages(count));
    }

    public async Task<string?> InsertImage(ImageInsertDTO image)
    {
        var result = await _imageService.SaveImage(image.Image, image.ContentId.ToString());
        if (result.Item1 is null || result.Item1.ToString() == string.Empty || result.Item2 == 0)
            return string.Empty;
        return await _repository.InsertImage(image, result.Item1);
    }

    public async Task<IEnumerable<string?>> InsertMultipleImages(ImagesInsertDTO images)
    {
        if (images is null || images.Image is null)
            return new List<string>();

        var result = new List<string?>();
        var image = new ImageInsertDTO() { ContentId = images.ContentId };
        foreach (var file in images.Image)
        {
            image.Image = file;
            result.Add(await InsertImage(image));
        }

        return result;
    }

    public async Task<string?> UpdateImage(ImageUpdateDTO image)
    {
        return await _repository.UpdateImage(image);
    }

    public async Task<bool> DeleteImages(IEnumerable<Guid> ids)
    {
        foreach (var id in ids)
        {
            var image = await GetImage(id);
            if (image is null || image.Name is null)
                continue;

            _imageService.DeleteImage(image.ContentId.ToString(), image.Name);
            await _repository.DeleteImage(id);
        }
        return true;
    }
}
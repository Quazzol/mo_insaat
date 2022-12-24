using Backend.Connection;
using Backend.DTOs.Request;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class ImageLibraryRepository : IImageLibraryRepository
{
    private readonly AppDbContext _context;

    public ImageLibraryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ImageLibraryModel?> GetImage(Guid id)
    {
        return await _context.Images!.Include(q => q.Content).FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IEnumerable<ImageLibraryModel?>> GetImages(Guid contentId)
    {
        var images = await _context.Images!.Where(q => q.ContentId == contentId).OrderBy(q => q.SortOrder).Include(q => q.Content).ToListAsync();
        return images is null ? new List<ImageLibraryModel>() : images;
    }

    public async Task<IEnumerable<ImageLibraryModel?>> GetCoverImages(int count)
    {
        var images = await _context.Images!.Where(q => q.IsCover).OrderBy(q => q.SortOrder).Include(q => q.Content).Take(count).ToListAsync();
        return images is null ? new List<ImageLibraryModel>() : images;
    }

    public async Task<string?> InsertImage(ImageInsertDTO image, string name)
    {
        var lastImage = await _context.Images!.OrderByDescending(q => q.SortOrder).FirstOrDefaultAsync(q => q.ContentId == image.ContentId);
        var sortOrder = lastImage is null ? 1 : lastImage.SortOrder + 1;

        await CreateAndAddModel(name, sortOrder, image.IsCover, image.ContentId);
        await _context.SaveChangesAsync();

        return name;
    }

    public async Task<string?> UpdateImage(ImageUpdateDTO image)
    {
        var existingImage = await _context.Images!.FirstOrDefaultAsync(q => q.Id == image.Id);
        if (existingImage is null)
            return string.Empty;

        existingImage.SortOrder = image.SortOrder == 0 ? existingImage.SortOrder : image.SortOrder;
        existingImage.IsCover = image.IsCover;

        await _context.SaveChangesAsync();

        return existingImage.Name;
    }

    public async Task<bool> DeleteImage(Guid id)
    {
        var model = _context.Images!.FirstOrDefault(q => q.Id == id);
        if (model is null)
            return true;
        _context.Images!.Remove(model);
        return await _context.SaveChangesAsync() > 0;
    }

    private async Task CreateAndAddModel(string? name, int sortOrder, bool isCover, Guid contentId)
    {
        var model = new ImageLibraryModel()
        {
            Id = Guid.NewGuid(),
            Name = name,
            SortOrder = sortOrder,
            IsCover = isCover,
            ContentId = contentId
        };

        await _context.Images!.AddAsync(model);
    }

}
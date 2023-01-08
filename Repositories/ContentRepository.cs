using AutoMapper;
using Backend.Connection;
using Backend.Datas.Enums;
using Backend.DTOs.Request;
using Backend.DTOs.Response;
using Backend.Misc;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class ContentRepository : IContentRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ContentRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ContentModel?> Get(Guid id)
    {
        return await _context.Contents!
            .Include(q => q.Images)
            .Include(q => q.HeaderContent)
            .FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IEnumerable<ContentModel?>> GetAll(Guid? headerContentId)
    {
        return await _context.Contents!
            .Where(q => q.HeaderContentId == headerContentId)
            .Include(q => q.Images)
            .Include(q => q.HeaderContent)
            .OrderBy(q => q.SortOrder)
            .ToListAsync();
    }

    public async Task<IEnumerable<ContentModel?>> GetAll(string languageCode, int page, int count)
    {
        return await _context.Contents!
            .Where(q => q.LanguageCode == languageCode && q.HeaderContentId != null)
            .Include(q => q.Images)
            .Include(q => q.HeaderContent)
            .OrderBy(q => q.HeaderContentId)
            .ThenBy(q => q.SortOrder)
            .Skip(page * count)
            .Take(count)
            .ToListAsync();
    }

    public async Task<IEnumerable<ContentTitleDTO?>> GetAllTitle(string languageCode)
    {
        return await _context.Contents!
            .Where(q => q.LanguageCode == languageCode && !q.IsSubContent && !q.IsImageLibrary)
            .OrderBy(q => q.SortOrder)
            .Select(q =>
        new ContentTitleDTO()
        {
            Id = q.Id,
            Name = q.Name,
            Link = q.Link,
            SortOrder = q.SortOrder,
            HeaderContentId = q.HeaderContentId
        }).ToListAsync();
    }

    public async Task<IEnumerable<ContentModel>> GetVisibleOnIndex(string languageCode)
    {
        return await _context.Contents!
            .Where(q => q.IsVisibleOnIndex && q.LanguageCode == languageCode)
            .Include(q => q.Images)
            .Include(q => q.HeaderContent)
            .OrderBy(q => q.SortOrder)
            .ToListAsync();
    }

    public async Task<ContentModel?> Insert(ContentInsertDTO content)
    {
        var existingModel = await _context.Contents!.FirstOrDefaultAsync(q => q.Name == content.Name);
        if (existingModel != null)
            return existingModel;

        var link = string.Empty;
        if (!content.HeaderContentId.IsEmpty())
        {
            var headerModel = await _context.Contents!.FirstOrDefaultAsync(q => q.Id == content.HeaderContentId);
            if (headerModel == null)
                throw new ArgumentException("No content found with the given ContentHeaderID");
            link = headerModel.Link;
        }

        var lastSortOrder = await _context.Contents!.Where(q => q.HeaderContentId == content.HeaderContentId).OrderByDescending(q => q.SortOrder).FirstOrDefaultAsync();
        var model = _mapper.Map<ContentModel>(content);
        model.Id = Guid.NewGuid();
        model.Link = string.Format("{0}{1}", link, content.Name!.Linkify());
        model.SortOrder = lastSortOrder is null ? 1 : lastSortOrder.SortOrder + 1;

        await _context.Contents!.AddAsync(model);
        await _context.SaveChangesAsync();

        return model;
    }

    public async Task<ContentModel?> Update(ContentUpdateDTO content)
    {
        var existingModel = await _context.Contents!.FirstOrDefaultAsync(q => q.Id == content.Id);
        if (existingModel is null)
            return null;

        if (!existingModel.IsFixed)
        {
            existingModel.Name = content.Name.IsEmpty() ? existingModel.Name : content.Name;
            existingModel.Link = content.Name.IsEmpty() ? existingModel.Link : content.Name!.Linkify();
            existingModel.IsSubContent = content.IsSubContent == null ? existingModel.IsSubContent : (bool)content.IsSubContent;
            existingModel.IsImageLibrary = content.IsImageLibrary == null ? existingModel.IsImageLibrary : (bool)content.IsImageLibrary;
            existingModel.IsVisibleOnIndex = content.IsVisibleOnIndex == null ? existingModel.IsVisibleOnIndex : (bool)content.IsVisibleOnIndex;
            existingModel.IsCompleted = content.IsCompleted == null ? existingModel.IsCompleted : (bool)content.IsCompleted;
            existingModel.SortOrder = content.SortOrder == 0 ? existingModel.SortOrder : content.SortOrder;
            existingModel.IsCompleted = content.IsCompleted == null ? existingModel.IsCompleted : (bool)content.IsCompleted;
        }

        existingModel.LanguageCode = content.LanguageCode.IsEmpty() ? existingModel.LanguageCode : content.LanguageCode;
        existingModel.Content = content.Content.IsEmpty() ? existingModel.Content : content.Content;

        await _context.SaveChangesAsync();

        return existingModel;
    }

    public async Task<bool> Delete(Guid id)
    {
        var content = await _context.Contents!.FirstOrDefaultAsync(q => q.Id == id);
        if (content is null || content.IsFixed)
            return false;
        _context.Contents!.Remove(content);
        await _context.SaveChangesAsync();
        return true;
    }

}
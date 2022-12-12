using AutoMapper;
using Backend.Connection;
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
        return await _context.Contents!.FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IEnumerable<ContentModel?>> GetAll(Guid? headerContentId)
    {
        return await _context.Contents!.Where(q => q.HeaderContentId == headerContentId).ToListAsync();
    }

    public async Task<IEnumerable<ContentTitleDTO?>> GetAllTitle(string languageCode)
    {
        return await _context.Contents!.Where(q => q.LanguageCode == languageCode).Select(q =>
        new ContentTitleDTO()
        {
            Id = q.Id,
            Name = q.Name,
            Link = q.Link,
            SortOrder = q.SortOrder,
            HeaderContentId = q.HeaderContentId
        }).ToListAsync();
    }

    public async Task<IEnumerable<ContentModel>> GetVisibleOnMainPage(string languageCode)
    {
        return await _context.Contents!.Where(q => q.VisibleOnMain && q.LanguageCode == languageCode && q.Content != null).ToListAsync();
    }

    public async Task<ContentModel?> Insert(ContentInsertDTO content)
    {
        var existingModel = await _context.Contents!.FirstOrDefaultAsync(q => q.Name == content.Name);
        if (existingModel != null)
            return existingModel;

        var lastSortOrder = await _context.Contents!.Where(q => q.HeaderContentId == content.HeaderContentId).OrderByDescending(q => q.SortOrder).FirstOrDefaultAsync();
        var model = _mapper.Map<ContentModel>(content);
        model.Id = Guid.NewGuid();
        model.Link = content.Name!.Linkify();
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

        existingModel.LanguageCode = content.LanguageCode;
        existingModel.Name = content.Name;
        existingModel.Link = content.Name!.Linkify();
        existingModel.Content = content.Content;
        existingModel.ImageLibrary = content.ImageLibrary;
        existingModel.VisibleOnMain = content.VisibleOnMain;
        existingModel.SortOrder = content.SortOrder;

        await _context.SaveChangesAsync();

        return existingModel;
    }

    public async Task Delete(Guid id)
    {
        var content = await _context.Contents!.FirstOrDefaultAsync(q => q.Id == id);
        if (content is null)
            return;
        _context.Contents!.Remove(content);
        await _context.SaveChangesAsync();
    }

}
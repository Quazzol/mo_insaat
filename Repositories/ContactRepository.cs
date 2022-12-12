using AutoMapper;
using Backend.Connection;
using Backend.DTOs;
using Backend.DTOs.Request;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;

    public ContactRepository(AppDbContext context, IMapper mapper, IUserContext userContext)
    {
        _context = context;
        _mapper = mapper;
        _userContext = userContext;
    }

    public async Task<ContactModel?> Get(Guid id)
    {
        return await _context.Contacts!.FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<ContactModel?> Insert(ContactInsertDTO content)
    {
        var existingModel = await _context.Contacts!.FirstOrDefaultAsync(q => q.LanguageCode == content.LanguageCode);
        if (existingModel != null)
            return existingModel;

        var lastSortOrder = await _context.Contents!.OrderByDescending(q => q.SortOrder).FirstOrDefaultAsync();
        var model = _mapper.Map<ContactModel>(content);
        model.Id = Guid.NewGuid();
        model.SortOrder = lastSortOrder is null ? 1 : lastSortOrder.SortOrder + 1;

        await _context.Contacts!.AddAsync(model);
        await _context.SaveChangesAsync();

        return model;
    }

    public async Task<ContactModel?> Update(ContactUpdateDTO content)
    {
        var existingModel = await _context.Contacts!.FirstOrDefaultAsync(q => q.Id == content.Id);
        if (existingModel is null)
            return null;

        existingModel.LanguageCode = content.LanguageCode;
        existingModel.Content = content.Content;
        existingModel.SortOrder = content.SortOrder;

        await _context.SaveChangesAsync();

        return existingModel;
    }

    public async Task Delete(Guid id)
    {
        var contact = await _context.Contacts!.FirstOrDefaultAsync(q => q.Id == id);
        if (contact is null)
            return;
        _context.Contacts!.Remove(contact);
        await _context.SaveChangesAsync();
    }
}
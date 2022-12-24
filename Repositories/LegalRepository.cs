using AutoMapper;
using Backend.Connection;
using Backend.DTOs.Request;
using Backend.Misc;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class LegalRepository : ILegalRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public LegalRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LegalModel?> Get(Guid id)
    {
        return await _context.Legals!.FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IEnumerable<LegalModel?>> GetAll(string languageCode)
    {
        return await _context.Legals!.Where(q => q.LanguageCode == languageCode).OrderBy(q => q.Name).ToListAsync();
    }

    public async Task<LegalModel?> Update(LegalUpdateDTO info)
    {
        var existingModel = await _context.Legals!.FirstOrDefaultAsync(q => q.Id == info.Id);
        if (existingModel is null)
            return null;

        existingModel.LanguageCode = info.LanguageCode.IsEmpty() ? existingModel.LanguageCode : info.LanguageCode;
        existingModel.Name = info.Name.IsEmpty() ? existingModel.Name : info.Name;
        existingModel.Link = info.Name.IsEmpty() ? existingModel.Link : info.Name.Linkify();
        existingModel.Content = info.Content.IsEmpty() ? existingModel.Content : info.Content;

        await _context.SaveChangesAsync();

        return existingModel;
    }
}
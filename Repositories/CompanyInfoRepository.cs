using AutoMapper;
using Backend.Connection;
using Backend.DTOs.Request;
using Backend.Misc;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class CompanyInfoRepository : ICompanyInfoRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CompanyInfoRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CompanyInfoModel?> Get(Guid id)
    {
        return await _context.CompanyInfos!.FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IEnumerable<CompanyInfoModel?>> GetAll(string languageCode)
    {
        return await _context.CompanyInfos!.Where(q => q.LanguageCode == languageCode).OrderBy(q => q.Type).ToListAsync();
    }

    public async Task<CompanyInfoModel?> Update(CompanyInfoUpdateDTO info)
    {
        var existingModel = await _context.CompanyInfos!.FirstOrDefaultAsync(q => q.Id == info.Id);
        if (existingModel is null)
            return null;

        existingModel.LanguageCode = info.LanguageCode.IsEmpty() ? existingModel.LanguageCode : info.LanguageCode;
        existingModel.Content = info.Content;

        await _context.SaveChangesAsync();

        return existingModel;
    }
}
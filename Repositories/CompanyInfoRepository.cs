using AutoMapper;
using Backend.Connection;
using Backend.Datas.Enums;
using Backend.DTOs;
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

    public async Task<CompanyInfoModel?> Insert(CompanyInfoInsertDTO info)
    {
        var existingModel = await _context.CompanyInfos!.FirstOrDefaultAsync(q => q.Type == info.Type);
        if (existingModel != null)
            return existingModel;

        var model = _mapper.Map<CompanyInfoModel>(info);
        model.Id = Guid.NewGuid();

        await _context.CompanyInfos!.AddAsync(model);
        await _context.SaveChangesAsync();

        return model;
    }

    public async Task<CompanyInfoModel?> Update(CompanyInfoUpdateDTO info)
    {
        var existingModel = await _context.CompanyInfos!.FirstOrDefaultAsync(q => q.Id == info.Id);
        if (existingModel is null)
            return null;

        existingModel.LanguageCode = info.LanguageCode.IsEmpty() ? existingModel.LanguageCode : info.LanguageCode;
        existingModel.Type = info.Type == CompanyInfoType.None ? existingModel.Type : info.Type;
        existingModel.Content = info.Content.IsEmpty() ? existingModel.Content : info.Content;

        await _context.SaveChangesAsync();

        return existingModel;
    }
}
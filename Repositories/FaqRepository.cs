using AutoMapper;
using Backend.Connection;
using Backend.DTOs.Request;
using Backend.Misc;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class FaqRepository : IFaqRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public FaqRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<FaqModel?> Get(Guid id)
    {
        return await _context.Faqs!.FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IEnumerable<FaqModel?>> GetAll(string languageCode, int count)
    {
        return await _context.Faqs!.Where(q => q.LanguageCode == languageCode).OrderBy(q => q.SortOrder).Take(count).ToListAsync();
    }

    public async Task<FaqModel?> Insert(FaqInsertDTO faq)
    {
        var lastSortOrder = await _context.Faqs!.OrderByDescending(q => q.SortOrder).FirstOrDefaultAsync();
        var model = _mapper.Map<FaqModel>(faq);
        model.Id = Guid.NewGuid();
        model.SortOrder = lastSortOrder is null ? 1 : lastSortOrder.SortOrder + 1;

        await _context.Faqs!.AddAsync(model);
        await _context.SaveChangesAsync();

        return model;
    }

    public async Task<FaqModel?> Update(FaqUpdateDTO faq)
    {
        var existingModel = await _context.Faqs!.FirstOrDefaultAsync(q => q.Id == faq.Id);
        if (existingModel is null)
            return null;

        existingModel.LanguageCode = faq.LanguageCode.IsEmpty() ? existingModel.LanguageCode : faq.LanguageCode;
        existingModel.Question = faq.Question.IsEmpty() ? existingModel.Question : faq.Question;
        existingModel.Answer = faq.Answer.IsEmpty() ? existingModel.Answer : faq.Answer;
        existingModel.SortOrder = faq.SortOrder == 0 ? existingModel.SortOrder : faq.SortOrder;

        await _context.SaveChangesAsync();

        return existingModel;
    }

    public async Task Delete(Guid id)
    {
        var Faq = await _context.Faqs!.FirstOrDefaultAsync(q => q.Id == id);
        if (Faq is null)
            return;
        _context.Faqs!.Remove(Faq);
        await _context.SaveChangesAsync();
    }

}
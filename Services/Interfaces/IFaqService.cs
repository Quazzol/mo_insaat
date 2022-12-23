using Backend.DTOs.Request;
using Backend.DTOs.Response;

namespace Backend.Services.Interfaces;

public interface IFaqService
{
    public Task<FaqDTO?> Get(Guid id);
    public Task<IEnumerable<FaqDTO?>> GetAll(string languageCode, int count);
    public Task<FaqDTO?> Insert(FaqInsertDTO Faq);
    public Task<FaqDTO?> Update(FaqUpdateDTO Faq);
    public Task Delete(Guid id);
}
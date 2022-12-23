using Backend.DTOs.Request;
using Backend.Models;

namespace Backend.Repositories.Interfaces;

public interface IFaqRepository
{
    public Task<FaqModel?> Get(Guid id);
    public Task<IEnumerable<FaqModel?>> GetAll(string languageCode, int count);
    public Task<FaqModel?> Insert(FaqInsertDTO content);
    public Task<FaqModel?> Update(FaqUpdateDTO content);
    public Task Delete(Guid id);
}
using Backend.Datas.Enums;
using Backend.DTOs.Request;
using Backend.Models;

namespace Backend.Repositories.Interfaces;

public interface ILegalRepository
{
    public Task<LegalModel?> Get(Guid id);
    public Task<IEnumerable<LegalModel?>> GetAll(string languageCode);
    public Task<LegalModel?> Update(LegalUpdateDTO content);
}
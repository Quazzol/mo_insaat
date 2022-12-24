using Backend.DTOs.Request;
using Backend.DTOs.Response;

namespace Backend.Services.Interfaces;

public interface ILegalService
{
    public Task<LegalDTO?> Get(Guid id);
    public Task<IEnumerable<LegalDTO?>> GetAll(string languageCode);
    public Task<LegalDTO?> Update(LegalUpdateDTO content);
}
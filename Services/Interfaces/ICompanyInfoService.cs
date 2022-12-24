using Backend.DTOs.Request;
using Backend.DTOs.Response;

namespace Backend.Services.Interfaces;

public interface ICompanyInfoService
{
    public Task<CompanyInfoDTO?> Get(Guid id);
    public Task<IEnumerable<CompanyInfoDTO?>> GetAll(string languageCode);
    public Task<CompanyInfoDTO?> Update(CompanyInfoUpdateDTO content);
}
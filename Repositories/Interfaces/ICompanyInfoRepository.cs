using Backend.DTOs.Request;
using Backend.Models;

namespace Backend.Repositories.Interfaces;

public interface ICompanyInfoRepository
{
    public Task<CompanyInfoModel?> Get(Guid id);
    public Task<IEnumerable<CompanyInfoModel?>> GetAll(string languageCode);
    public Task<CompanyInfoModel?> Insert(CompanyInfoInsertDTO info);
    public Task<CompanyInfoModel?> Update(CompanyInfoUpdateDTO info);
}
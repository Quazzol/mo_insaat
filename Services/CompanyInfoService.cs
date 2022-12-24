using AutoMapper;
using Backend.DTOs.Request;
using Backend.DTOs.Response;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;

namespace Backend.Services;

public class CompanyInfoService : ICompanyInfoService
{
    private readonly ICompanyInfoRepository _repository;
    private readonly IMapper _mapper;

    public CompanyInfoService(ICompanyInfoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CompanyInfoDTO?> Get(Guid id)
    {
        return _mapper.Map<CompanyInfoDTO>(await _repository.Get(id));
    }

    public async Task<IEnumerable<CompanyInfoDTO?>> GetAll(string languageCode)
    {
        return _mapper.Map<IEnumerable<CompanyInfoDTO>>(await _repository.GetAll(languageCode));
    }

    public async Task<CompanyInfoDTO?> Update(CompanyInfoUpdateDTO content)
    {
        return _mapper.Map<CompanyInfoDTO>(await _repository.Update(content));
    }
}
using AutoMapper;
using Backend.DTOs.Request;
using Backend.DTOs.Response;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;

namespace Backend.Services;

public class LegalService : ILegalService
{
    private readonly ILegalRepository _repository;
    private readonly IMapper _mapper;

    public LegalService(ILegalRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<LegalDTO?> Get(Guid id)
    {
        return _mapper.Map<LegalDTO>(await _repository.Get(id));
    }

    public async Task<IEnumerable<LegalDTO?>> GetAll(string languageCode)
    {
        return _mapper.Map<IEnumerable<LegalDTO>>(await _repository.GetAll(languageCode));
    }

    public async Task<LegalDTO?> Insert(LegalInsertDTO content)
    {
        return _mapper.Map<LegalDTO>(await _repository.Insert(content));
    }

    public async Task<LegalDTO?> Update(LegalUpdateDTO content)
    {
        return _mapper.Map<LegalDTO>(await _repository.Update(content));
    }
}
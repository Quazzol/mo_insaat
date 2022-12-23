using AutoMapper;
using Backend.DTOs;
using Backend.DTOs.Request;
using Backend.DTOs.Response;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;

namespace Backend.Services;

public class FaqService : IFaqService
{
    private readonly IFaqRepository _repository;
    private readonly IMapper _mapper;

    public FaqService(IFaqRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<FaqDTO?> Get(Guid id)
    {
        return _mapper.Map<FaqDTO>(await _repository.Get(id));
    }

    public async Task<IEnumerable<FaqDTO?>> GetAll(string languageCode, int count)
    {
        return _mapper.Map<IEnumerable<FaqDTO>>(await _repository.GetAll(languageCode, count));
    }

    public async Task<FaqDTO?> Insert(FaqInsertDTO Faq)
    {
        return _mapper.Map<FaqDTO>(await _repository.Insert(Faq));
    }

    public async Task<FaqDTO?> Update(FaqUpdateDTO Faq)
    {
        return _mapper.Map<FaqDTO>(await _repository.Update(Faq));
    }

    public async Task Delete(Guid id)
    {
        await _repository.Delete(id);
    }

}
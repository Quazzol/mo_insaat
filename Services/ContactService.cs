using AutoMapper;
using Backend.DTOs.Request;
using Backend.DTOs.Response;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;

namespace Backend.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _repository;
    private readonly IMapper _mapper;

    public ContactService(IContactRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ContactDTO?> Get(Guid id)
    {
        return _mapper.Map<ContactDTO>(await _repository.Get(id));
    }

    public async Task<ContactDTO?> Insert(ContactInsertDTO content)
    {
        return _mapper.Map<ContactDTO>(await _repository.Insert(content));
    }

    public async Task<ContactDTO?> Update(ContactUpdateDTO content)
    {
        return _mapper.Map<ContactDTO>(await _repository.Update(content));
    }

    public async Task Delete(Guid id)
    {
        await _repository.Delete(id);
    }
}
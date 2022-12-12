using Backend.DTOs.Request;
using Backend.DTOs.Response;

namespace Backend.Services.Interfaces;

public interface IContactService
{
    public Task<ContactDTO?> Get(Guid id);
    public Task<ContactDTO?> Insert(ContactInsertDTO content);
    public Task<ContactDTO?> Update(ContactUpdateDTO content);
    public Task Delete(Guid id);
}
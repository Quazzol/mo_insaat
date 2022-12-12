using Backend.DTOs.Request;
using Backend.Models;

namespace Backend.Repositories.Interfaces;

public interface IContactRepository
{
    public Task<ContactModel?> Get(Guid id);
    public Task<ContactModel?> Insert(ContactInsertDTO content);
    public Task<ContactModel?> Update(ContactUpdateDTO content);
    public Task Delete(Guid id);
}
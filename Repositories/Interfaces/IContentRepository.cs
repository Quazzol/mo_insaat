using Backend.DTOs.Request;
using Backend.DTOs.Response;
using Backend.Models;

namespace Backend.Repositories.Interfaces;

public interface IContentRepository
{
    public Task<ContentModel?> Get(Guid id);
    public Task<IEnumerable<ContentModel?>> GetAll(Guid? contentTypeId);
    public Task<IEnumerable<ContentModel?>> GetAll(string languageCode, int page, int count);
    public Task<IEnumerable<ContentTitleDTO?>> GetAllTitle(string languageCode);
    public Task<IEnumerable<ContentModel>> GetVisibleOnIndex(string languageCode);
    public Task<ContentModel?> Insert(ContentInsertDTO content);
    public Task<ContentModel?> Update(ContentUpdateDTO content);
    public Task<bool> Delete(Guid id);
}
using AutoMapper;
using Backend.Datas.Enums;
using Backend.DTOs;
using Backend.DTOs.Request;
using Backend.DTOs.Response;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;

namespace Backend.Services;

public class ContentService : IContentService
{
    private readonly IContentRepository _repository;
    private readonly IMapper _mapper;

    public ContentService(IContentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ContentDTO?> Get(Guid id)
    {
        return _mapper.Map<ContentDTO>(await _repository.Get(id));
    }

    public async Task<IEnumerable<ContentDTO?>> GetAll(Guid? contentHeaderId)
    {
        return _mapper.Map<IEnumerable<ContentDTO>>(await _repository.GetAll(contentHeaderId));
    }

    public async Task<IEnumerable<ContentDTO?>> GetAll(string languageCode, int page, int count)
    {
        return _mapper.Map<IEnumerable<ContentDTO>>(await _repository.GetAll(languageCode, page, count));
    }

    public async Task<IEnumerable<MenuItem?>> GetAllTitle(string languageCode)
    {
        var menuItems = (await _repository.GetAllTitle(languageCode)).Select(q => new MenuItem() { Title = q }).OrderBy(q => q.Title.SortOrder).ToList();
        var idsToRemove = new List<Guid?>();

        foreach (var menuItem in menuItems)
        {
            if (menuItem?.Title?.HeaderContentId == null)
                continue;

            var headerMenuItem = menuItems.First(q => q.Title?.Id == menuItem.Title.HeaderContentId);
            if (headerMenuItem == null)
                continue;
            if (headerMenuItem.SubTitles == null)
                headerMenuItem.SubTitles = new List<MenuItem>();
            headerMenuItem.SubTitles.Add(menuItem);
            idsToRemove.Add(menuItem.Title.Id);
        }
        return menuItems.Where(q => !idsToRemove.Contains(q.Title.Id));
    }

    public async Task<IEnumerable<ContentDTO>> GetVisibleOnIndex(string languageCode)
    {
        return _mapper.Map<IEnumerable<ContentDTO>>(await _repository.GetVisibleOnIndex(languageCode));
    }

    public async Task<ContentDTO?> Insert(ContentInsertDTO content)
    {
        return _mapper.Map<ContentDTO>(await _repository.Insert(content));
    }

    public async Task<ContentTitleDTO?> InsertContentTitle(ContentTitleInsertDTO title)
    {
        return _mapper.Map<ContentTitleDTO>(await _repository.Insert(_mapper.Map<ContentInsertDTO>(title)));
    }

    public async Task<ContentDTO?> Update(ContentUpdateDTO content)
    {
        return _mapper.Map<ContentDTO>(await _repository.Update(content));
    }

    public async Task<ContentTitleDTO?> UpdateContentTitle(ContentTitleUpdateDTO title)
    {
        return _mapper.Map<ContentTitleDTO>(await _repository.Update(_mapper.Map<ContentUpdateDTO>(title)));
    }

    public async Task Delete(Guid id)
    {
        await _repository.Delete(id);
    }

}
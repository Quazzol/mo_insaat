using AutoMapper;
using Backend.DTOs.Request;
using Backend.DTOs.Response;
using Backend.Models;

namespace PlatformService.Profiles;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // Source -> Target
        CreateMap<ContactModel, ContactDTO>();
        CreateMap<ContentModel, ContentDTO>();
        CreateMap<ContentInsertDTO, ContentModel>();
        CreateMap<ContentTitleInsertDTO, ContentModel>();
        CreateMap<ContentModel, ContentTitleDTO>();
        CreateMap<ContentTitleInsertDTO, ContentInsertDTO>();
        CreateMap<ContentDTO, ContentTitleDTO>();
        CreateMap<ImageLibraryModel, ImageDTO>();
    }
}
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
        CreateMap<CompanyInfoModel, CompanyInfoDTO>();
        CreateMap<ContentModel, ContentDTO>();
        CreateMap<ContentInsertDTO, ContentModel>();
        CreateMap<ContentTitleInsertDTO, ContentModel>();
        CreateMap<ContentModel, ContentTitleDTO>();
        CreateMap<ContentTitleInsertDTO, ContentInsertDTO>();
        CreateMap<ContentTitleUpdateDTO, ContentUpdateDTO>();
        CreateMap<ContentUpdateDTO, ContentTitleDTO>();
        CreateMap<ContentDTO, ContentTitleDTO>();
        CreateMap<FaqModel, FaqDTO>();
        CreateMap<FaqInsertDTO, FaqModel>();
        CreateMap<LegalModel, LegalDTO>();
        CreateMap<LegalInsertDTO, LegalModel>();
        CreateMap<ImageLibraryModel, ImageDTO>()
            .ForMember(dest => dest.ContentName, source => source.MapFrom(i => i.Content!.Name))
            .ForMember(dest => dest.ContentLink, source => source.MapFrom(i => i.Content!.Link));
    }
}
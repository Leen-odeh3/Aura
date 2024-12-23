using Aura.Domain.DTOs.Story;
using Aura.Domain.Entities;
using AutoMapper;

namespace Aura.Api.Mapping;
public class StoryMappingProfile : Profile
{
    public StoryMappingProfile()
    {
        CreateMap<Story, StoryResponseDto>()
            .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.Image.ImagePath));

        CreateMap<CreateStoryDto, Story>()
            .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));
    }
}
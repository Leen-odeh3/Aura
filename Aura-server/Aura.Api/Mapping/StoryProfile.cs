using Aura.Domain.DTOs.Story;
using Aura.Domain.Entities;
using AutoMapper;

namespace Aura.Api.Mapping;
 public class StoryProfile : Profile
    {
        public StoryProfile()
        {
            CreateMap<CreateStoryDto, Story>()
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => DateTime.UtcNow)) 
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false)); 

            CreateMap<Story, StoryResponseDto>()
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.Image != null ? src.Image.ImagePath : string.Empty)); 
        }
    }

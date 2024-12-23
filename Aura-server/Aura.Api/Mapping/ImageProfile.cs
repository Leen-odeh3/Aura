using Aura.Domain.DTOs.Image;
using Aura.Domain.Entities;
using AutoMapper;

namespace Aura.Api.Mapping;
public class ImageProfile : Profile
{
    public ImageProfile()
    {
        CreateMap<Image, ImageResponseDto>();
    }
}
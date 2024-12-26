using Aura.Domain.DTOs.Message;
using Aura.Domain.Entities;
using AutoMapper;

namespace Aura.Api.Mapping;
public class MessageProfile : Profile
{
    public MessageProfile() { CreateMap<Message, MessageResponseDto>(); }
}
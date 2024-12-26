using Aura.Domain.DTOs.Message;
using Aura.Domain.Entities;
using AutoMapper;

namespace Aura.Api.Mapping;
public class ChatProfile : Profile
{
    public ChatProfile()
    {
        CreateMap<ChatWithLastMessage, ChatWithLastMessageResponseDto>();
    }
}
using Aura.Domain.DTOs.User;

namespace Aura.Domain.DTOs.Message;
public class ChatWithLastMessageResponseDto
{
    public UserResponseDto User { get; set; }
    public MessageResponseDto LastMessage { get; set; }
}
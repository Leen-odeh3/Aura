using Aura.Domain.DTOs.Message;
namespace Aura.Application.Abstracts;
public interface IMessageService
{
    Task<MessageResponseDto> StorePrivateMessage(int destinationUserId, string textMessage);
    Task<MessagesWithPaginationResponseDto> GetPrivateMessages(
        DateTime? pageDate,
        int pageSize,
        int firstUserId,
        int secoundUserId);
    Task<IEnumerable<ChatWithLastMessageResponseDto>> GetRecentChatsForUser(int userId);
}
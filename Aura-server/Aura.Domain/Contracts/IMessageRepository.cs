using Aura.Domain.Entities;

namespace Aura.Domain.Contracts;
public interface IMessageRepository
{
    Task AddAsync(Message message);
    void Delete(Message message);
    Task<Tuple<List<Message>, bool>> GetPrivateMessagesForPrivateChat(
        DateTime pageDate,
        int pageSize,
        int firstUserId,
        int secoundUserId);

    Task<IEnumerable<ChatWithLastMessage>> GetRecentChatsForUser(int userId);
}
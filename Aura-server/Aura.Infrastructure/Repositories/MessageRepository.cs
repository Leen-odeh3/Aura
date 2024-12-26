
using Aura.Domain.Contracts;
using Aura.Domain.Entities;
using Aura.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Aura.Infrastructure.Repositories;
public class MessageRepository : IMessageRepository
{
    private readonly AppDbContext context;
    public MessageRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task AddAsync(Message message)
    {
        await context.Messages.AddAsync(message);
    }

    public void Delete(Message message)
    {
        context.Messages.Remove(message);
    }

    public async Task<Tuple<List<Message>, bool>> GetPrivateMessagesForPrivateChat(
        DateTime pageDate,
        int pageSize,
        int firstUserId,
        int secoundUserId)
    {
        var messages = context.Messages
            .Where(m => (m.SenderId == firstUserId && m.ReceiverId == secoundUserId) ||
                       (m.SenderId == secoundUserId && m.ReceiverId == firstUserId))
            .Where(m => m.CreationDate < pageDate)
            .AsQueryable();
        var messagesCount = await messages.CountAsync();
        var isThereMore = messagesCount > pageSize;
        var messagesList = await messages
            .OrderByDescending(c => c.CreationDate)
            .Take(pageSize)
            .OrderBy(c => c.CreationDate)
            .ToListAsync();
        var result = Tuple.Create(messagesList, isThereMore);
        return result;
    }

    public async Task<IEnumerable<ChatWithLastMessage>> GetRecentChatsForUser(int userId)
    {
        var recentChatsWithLastMessages = await context.Messages
            .Where(m => m.SenderId == userId || m.ReceiverId == userId)
            .GroupBy(m => m.SenderId == userId ? m.ReceiverId : m.SenderId)
            .OrderByDescending(g => g.Max(m => m.CreationDate))
            .Take(10)
            .Select(g => new ChatWithLastMessage
            {
                User = context.Users.Where(u => u.Id == g.Key).Include(u => u.Image).First(),
                LastMessage = g.OrderByDescending(msg => msg.CreationDate).First()
            })
            .ToListAsync();
        return recentChatsWithLastMessages;
    }

}
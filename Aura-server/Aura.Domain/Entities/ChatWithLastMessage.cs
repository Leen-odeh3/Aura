namespace Aura.Domain.Entities;
public class ChatWithLastMessage
{
    public User User { get; set; }
    public Message LastMessage { get; set; }
}
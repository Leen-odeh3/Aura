using Aura.Application.Abstracts;
using Aura.Application.Abstracts.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRHub = Microsoft.AspNetCore.SignalR.Hub;


namespace Aura.Application.Hub;
[Authorize]
public class ChatHub : SignalRHub
{
    private readonly IAuthenticatedUserService authenticatedUserService;
    private readonly IMessageService privateMessageService;
    private static readonly Dictionary<int, string> activeUsers = new();

    public ChatHub(
        IAuthenticatedUserService authenticatedUserService,
        IMessageService privateMessageService)
    {
        this.authenticatedUserService = authenticatedUserService;
        this.privateMessageService = privateMessageService;
    }

    public async Task SendMessageToAll(int userId, string message)
    {
        await Clients.Others.SendAsync("ReceiveMessage", userId, message);
    }

    public async Task SendMessageToUser(int userId, string message)
    {
        var Storedmessage = await privateMessageService.StorePrivateMessage(userId, message);
        if (activeUsers.ContainsKey(userId))
        {
            var username = authenticatedUserService.GetAuthenticatedUsername();
            await Clients.Client(activeUsers[userId]).SendAsync("ReceiveMessage", Storedmessage, username);
        }
    }

    public async Task AddUser(int userId, string connectionId)
    {
        activeUsers.Add(userId, connectionId);
    }

    public string GetConnectionId()
    {
        return Context.ConnectionId;
    }

    public List<int> GetActiveUserIds()
    {
        return activeUsers.Keys.ToList();
    }

    public override async Task OnConnectedAsync()
    {
        var connectionId = GetConnectionId();
        var userId = authenticatedUserService.GetAuthenticatedUserId();
        if (!activeUsers.ContainsKey(userId))
        {
            activeUsers.Add(userId, connectionId);
        }
        await Clients.All.SendAsync("ReceiveActiveUsers", GetActiveUserIds());
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var connectionId = GetConnectionId();

        foreach (var user in activeUsers)
        {
            if (user.Value == connectionId)
            {
                activeUsers.Remove(user.Key);
                break;
            }
        }
        await Clients.All.SendAsync("ReceiveActiveUsers", GetActiveUserIds());
        await base.OnDisconnectedAsync(exception);
    }
}
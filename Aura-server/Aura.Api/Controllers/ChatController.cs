using Aura.Application.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Aura.Api.Controllers;

[Route("api/users/{userId}/recent-chats")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IMessageService MessageService;

    public ChatController(IMessageService MessageService)
    {
        this.MessageService = MessageService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetRecentChatsForUser(int userId)
    {
        var result = await MessageService.GetRecentChatsForUser(userId);
        return Ok(result);
    }
}

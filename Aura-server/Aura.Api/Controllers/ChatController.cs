using Aura.Application.Abstracts;
using Aura.Domain.DTOs.Message;
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

    [Authorize]
    [HttpPost("sendMessage")]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
    {
        var result = await MessageService.StorePrivateMessage(request.UserId, request.Message);
        if (result != null)
        {
            return Ok(new { message = "Message sent successfully", data = result });
        }
        return BadRequest("Failed to send message");
    }

}

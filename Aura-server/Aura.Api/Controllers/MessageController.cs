using Aura.Application.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aura.Api.Controllers;

[Route("api/private-messages")]
[ApiController]
public class PrivateMessagesController : ControllerBase
{
    private readonly IMessageService privateMessageService;

    public PrivateMessagesController(IMessageService privateMessageService)
    {
        this.privateMessageService = privateMessageService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetPrivateMessages(
        DateTime? pageDate,
        int pageSize,
        int firstUserId,
        int secoundUserId)
    {
        var result = await privateMessageService.GetPrivateMessages(pageDate, pageSize, firstUserId, secoundUserId);
        return Ok(result);
    }
}
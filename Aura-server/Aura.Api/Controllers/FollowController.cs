using Aura.Application.Abstracts;
using Aura.Application.Services;
using Aura.Domain.DTOs.Follow;
using Aura.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aura.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FollowController : ControllerBase
{
    private readonly IFollowService _followService;

    public FollowController(IFollowService followService)
    {
        _followService = followService;
    }

    [HttpPost("follow")]
    public async Task<ActionResult<FollowResponseDto>> FollowUser([FromBody] FollowRequestDto followRequestDto)
    {
        try
        {
            var response = await _followService.FollowUserAsync(followRequestDto);
            return Ok(response);
        }
        catch (BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}